using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserHomeService(ApplicationDbContext context) : IUserHomeService
{
    public async Task<ServiceResult<HomeDataDto>> GetHomeDataAsync()
    {
        var journalsCount = await context.Journals.CountAsync(j => j.JournalStatus == 1);
        var editorsCount = await context.Editors.CountAsync(e => e.EditorStatus == 1);
        var reviewersCount = await context.Reviewers.CountAsync(r => r.ReviewerStatus == 1);

        var recentArticles = await context.Articles
            .Where(a => a.ArticleStatus == 1)
            .OrderByDescending(a => a.ArticleId)
            .Take(6)
            .Select(a => new ArticleListItemDto
            {
                ArticleId = a.ArticleId,
                ArticleName = a.ArticleName,
                ArticleSeoName = a.ArticleSeoName,
                ArticlePdf = a.ArticlePdf,
                PublishedDate = a.PublishedDate,
                ArticleFor = a.ArticleFor,
                VolumeNo = a.VolumeNo,
                IssueNo = a.IssueNo
            })
            .ToListAsync();

        var articleIds = recentArticles.Select(x => x.ArticleId).ToList();

        var recentArticleAuthors = await context.ArticleAuthors
            .Where(x => articleIds.Contains(x.ArticleId))
            .Select(x => new
            {
                x.ArticleId,
                x.AuthorName
            })
            .ToListAsync();

        foreach (var article in recentArticles)
        {
            article.AllAuthorNames = string.Join(", ",
                recentArticleAuthors
                    .Where(x => x.ArticleId == article.ArticleId)
                    .Select(x => x.AuthorName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .Distinct()
            ) ?? string.Empty;
        }
        var collaborators = await context.Collaborators
            .Where(c => c.CollaboratorStatus == 1)
            .OrderBy(c => Guid.NewGuid())
            .Take(10)
            .Select(c => new CollaboratorPublicDto
            {
                CollaboratorId = c.CollaboratorId,
                CollaboratorName = c.CollaboratorName,
                CollaboratorUrl = c.CollaboratorUrl,
                CollaboratorImage = c.CollaboratorImage
            })
            .ToListAsync();

        var result = new HomeDataDto
        {
            JournalsCount = journalsCount,
            AdvisoryBoardCount = editorsCount + reviewersCount,
            RecentArticles = recentArticles,
            Collaborators = collaborators
        };

        return ServiceResult<HomeDataDto>.Ok(result);
    }
}
