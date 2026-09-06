using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// Public special issue detail endpoint.
/// </summary>
[Route("api/user/special-issues")]
public class SpecialIssuesController(IUserSpecialIssueService specialIssueService) : UserBaseController
{
    /// <summary>
    /// Returns the full detail of an active special issue including its editors.
    /// Also increments the view count each time this endpoint is called.
    /// Used on /special-issues/{slug}.
    /// </summary>
    [HttpGet(nameof(GetSpecialIssue))]
    public async Task<IActionResult> GetSpecialIssue(string slug)
        => ToResult(await specialIssueService.GetSpecialIssueBySlugAsync(slug));
}
