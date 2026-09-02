using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserSitemapService(ApplicationDbContext context) : IUserSitemapService
{
    public async Task<IEnumerable<string>> GetSitemapUrlsAsync(string baseUrl)
    {
        var urls = new List<string>();
        var base_ = baseUrl.TrimEnd('/');

        // Static pages
        urls.AddRange(new[]
        {
            $"{base_}/",
            $"{base_}/journals",
            $"{base_}/membership",
            $"{base_}/reprints-permissions",
            $"{base_}/open-access",
            $"{base_}/submit-manuscript",
            $"{base_}/authors",
            $"{base_}/editors",
            $"{base_}/about",
            $"{base_}/article-processing-charges",
            $"{base_}/abstracting-and-indexing",
            $"{base_}/licence",
            $"{base_}/open-peer-review",
            $"{base_}/orcid",
            $"{base_}/manuscript-preparation",
            $"{base_}/editing-translation-and-formatting",
            $"{base_}/editorial-work-flow",
            $"{base_}/plagiarism-checker",
            $"{base_}/waiver-policy"
        });

        // Journal-specific pages
        var journals = await context.Journals
            .Where(j => j.JournalStatus == 1 && j.JournalSeoName != null)
            .Select(j => new { j.JournalSeoName, j.JournalId })
            .ToListAsync();

        foreach (var journal in journals)
        {
            var seo = journal.JournalSeoName!;
            urls.Add($"{base_}/journals/{seo}");
            urls.Add($"{base_}/journals/{seo}/editorial-board");
            urls.Add($"{base_}/journals/{seo}/editor-profiles");
            urls.Add($"{base_}/journals/{seo}/articles-in-press");
            urls.Add($"{base_}/journals/{seo}/current-issues");
            urls.Add($"{base_}/journals/{seo}/archives");
            urls.Add($"{base_}/journals/{seo}/abstract-indexing");
            urls.Add($"{base_}/journals/{seo}/about-journal");
            urls.Add($"{base_}/submit-manuscript?e={journal.JournalId}");
        }

        // Article pages
        var articles = await context.Articles
            .Where(a => a.ArticleStatus == 1)
            .OrderByDescending(a => a.ArticleId)
            .Select(a => new { a.ArticleSeoName, a.ArticlePdf })
            .ToListAsync();

        foreach (var article in articles)
        {
            urls.Add($"{base_}/article/{article.ArticleSeoName}");
            if (!string.IsNullOrEmpty(article.ArticlePdf))
                urls.Add($"{base_}/uploads/articles/{article.ArticlePdf}");
        }

        return urls;
    }
}
