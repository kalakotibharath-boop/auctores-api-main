using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserArticleService(ApplicationDbContext context) : IUserArticleService
{
    // ── Full article detail page ───────────────────────────────────────────────

    public async Task<ServiceResult<ArticlePublicDetailDto>> GetArticleBySlugAsync(string seoName)
    {
        var article = await context.Articles
            .Where(a => a.ArticleStatus == 1 && a.ArticleSeoName == seoName)
            .Join(context.Journals, a => a.JournalId, j => j.JournalId,
                (a, j) => new ArticlePublicDetailDto
                {
                    ArticleId = a.ArticleId,
                    ArticleName = a.ArticleName,
                    ArticleSeoName = a.ArticleSeoName,
                    ArticleDoi = a.ArticleDoi,
                    ArticleAbstract = a.ArticleAbstract,
                    DisplayArticleAbstract = a.DisplayArticleAbstract,
                    ArticleInformation = a.ArticleInformation,
                    DisplayArticleInformation = a.DisplayArticleInformation,
                    AbstractKeywords = a.AbstractKeywords,
                    ArticlePdf = a.ArticlePdf,
                    CorrespondingAuthor = a.CorrespondingAuthor,
                    ReceivedDate = a.ReceivedDate,
                    AcceptedDate = a.AcceptedDate,
                    PublishedDate = a.PublishedDate,
                    Citation = a.Citation,
                    Copyright = a.Copyright,
                    ArticleFor = a.ArticleFor,
                    VolumeNo = a.VolumeNo,
                    IssueNo = a.IssueNo,
                    JournalId = a.JournalId,
                    JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName,
                    ArticleViews = a.ArticleViews,
                    ArticleDownloads = a.ArticleDownloads
                })
            .FirstOrDefaultAsync();

        if (article is null)
            return ServiceResult<ArticlePublicDetailDto>.NotFound("Article not found.");

        // Load related authors, details and references in parallel
        var authorsTask = await context.ArticleAuthors
            .Where(au => au.ArticleId == article.ArticleId)
            .OrderBy(au => au.AuthorNo)
            .Select(au => new ArticleAuthorPublicDto
            {
                AuthorName = au.AuthorName,
                AuthorNo = au.AuthorNo,
                AuthorSubhead = au.AuthorSubhead,
                AuthorEmail = au.AuthorEmail,
                AuthorOrcid = au.AuthorOrcid,
                AuthorAddress = au.AuthorAddress
            })
            .ToListAsync();

        var detailsTask = await context.ArticleDetails
            .Where(d => d.DetailsArticleId == article.ArticleId)
            .OrderBy(d => d.DetailsId)
            .Select(d => new ArticleDetailPublicDto
            {
                DetailsId = d.DetailsId,
                DetailsHeading = d.DetailsHeading,
                DisplayDetailsDescription = d.DisplayDetailsDescription
            })
            .ToListAsync();

        var referencesTask = await context.ArticleReferences
            .Where(r => r.ReferenceArticleId == article.ArticleId)
            .Select(r => new ArticleReferencePublicDto
            {
                ReferenceId = r.ReferenceId,
                ReferenceData = r.ReferenceData,
                ReferenceGoogleLink = r.ReferenceGoogleLink,
                ReferencePublisherLink = r.ReferencePublisherLink
            })
            .ToListAsync();

        //await Task.WhenAll(authorsTask, detailsTask, referencesTask);

        article.Authors = authorsTask;
        article.Details = detailsTask;
        article.References = referencesTask;

        return ServiceResult<ArticlePublicDetailDto>.Ok(article);
    }

    // ── Track PDF download ────────────────────────────────────────────────────

    public async Task<ServiceResult> IncrementDownloadCountAsync(long articleId)
    {
        int rows = await context.Articles
            .Where(a => a.ArticleId == articleId)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.ArticleDownloads, a => a.ArticleDownloads + 1));

        return rows > 0
            ? ServiceResult.Ok()
            : ServiceResult.NotFound("Article not found.");
    }

    // ── Track article view ────────────────────────────────────────────────────

    public async Task<ServiceResult> IncrementViewCountAsync(long articleId)
    {
        int rows = await context.Articles
            .Where(a => a.ArticleId == articleId)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.ArticleViews, a => a.ArticleViews + 1));

        return rows > 0
            ? ServiceResult.Ok()
            : ServiceResult.NotFound("Article not found.");
    }
}
