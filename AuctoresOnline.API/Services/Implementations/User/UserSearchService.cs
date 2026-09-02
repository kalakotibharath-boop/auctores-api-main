using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserSearchService(ApplicationDbContext context) : IUserSearchService
{
    public async Task<ServiceResult<IEnumerable<ArticleListItemDto>>> SearchArticlesAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return ServiceResult<IEnumerable<ArticleListItemDto>>.Fail("Keyword is required.");

        var normalised = keyword.Trim().ToLower();

        var articles = await context.Articles
            .Where(a => a.ArticleStatus == 1 && a.ArticleName.ToLower().Contains(normalised))
            .OrderByDescending(a => a.PublishedDate)
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

        if (articles.Count > 0)
        {
            var ids = articles.Select(a => a.ArticleId).ToList();
            var authorGroups = await context.ArticleAuthors
                .Where(au => ids.Contains(au.ArticleId))
                .OrderBy(au => au.ArticleId).ThenBy(au => au.AuthorNo)
                .Select(au => new { au.ArticleId, au.AuthorName })
                .ToListAsync();

            var lookup = authorGroups
                .GroupBy(x => x.ArticleId)
                .ToDictionary(g => g.Key, g => string.Join(", ", g.Select(x => x.AuthorName)));

            foreach (var article in articles)
                article.AllAuthorNames = lookup.GetValueOrDefault(article.ArticleId);
        }

        return ServiceResult<IEnumerable<ArticleListItemDto>>.Ok(articles);
    }
}
