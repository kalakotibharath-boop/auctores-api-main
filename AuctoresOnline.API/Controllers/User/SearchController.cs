using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// Public article search endpoint.
/// </summary>
[Route("api/user/search")]
public class SearchController(IUserSearchService searchService) : UserBaseController
{
    /// <summary>
    /// Searches active articles by title keyword.
    /// Results are ordered by most recent publication date.
    /// Used on the /search page.
    /// </summary>
    /// <param name="keyword">Search term to match against article titles.</param>
    [HttpGet(nameof(Search))]
    public async Task<IActionResult> Search([FromQuery] string keyword)
        => ToResult(await searchService.SearchArticlesAsync(keyword));
}
