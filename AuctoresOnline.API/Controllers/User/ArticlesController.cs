using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// Public article detail and engagement tracking endpoints.
/// </summary>
[Route("api/user/articles")]
public class ArticlesController(IUserArticleService articleService) : UserBaseController
{
    /// <summary>
    /// Returns the full article detail page data: article info, authors,
    /// content sections, and references.
    /// Used on /article/{seoName}.
    /// </summary>
    [HttpGet(nameof(GetArticle))]
    public async Task<IActionResult> GetArticle(string seoName)
        => ToResult(await articleService.GetArticleBySlugAsync(seoName));

    /// <summary>
    /// Increments the PDF download counter for the article.
    /// Call this when a user clicks the PDF download link.
    /// </summary>
    [HttpPost(nameof(TrackDownload))]
    public async Task<IActionResult> TrackDownload(long articleId)
        => ToResult(await articleService.IncrementDownloadCountAsync(articleId));

    /// <summary>
    /// Increments the view counter for the article.
    /// Call this when the article detail page loads.
    /// </summary>
    [HttpPost(nameof(TrackView))]
    public async Task<IActionResult> TrackView(long articleId)
        => ToResult(await articleService.IncrementViewCountAsync(articleId));
}
