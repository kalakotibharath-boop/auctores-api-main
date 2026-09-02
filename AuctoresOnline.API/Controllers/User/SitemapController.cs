using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// Sitemap endpoint — returns all public SEO-addressable URLs.
/// </summary>
[Route("api/user/sitemap")]
public class SitemapController(IUserSitemapService sitemapService) : UserBaseController
{
    /// <summary>
    /// Returns the full list of crawlable URLs for the site including static pages,
    /// all active journal sub-pages, and all active article URLs.
    /// Pass a baseUrl query parameter to control the domain prefix,
    /// otherwise the request's origin is used.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetSitemap([FromQuery] string? baseUrl)
    {
        var origin = baseUrl ?? $"{Request.Scheme}://{Request.Host}";
        var urls = await sitemapService.GetSitemapUrlsAsync(origin);
        return Ok(ApiResponse<IEnumerable<string>>.Ok(urls));
    }
}
