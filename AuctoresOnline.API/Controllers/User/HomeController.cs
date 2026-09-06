using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// Public home page data — journal stats, recent articles, and collaborators.
/// </summary>
[Route("api/user/home")]
public class HomeController(IUserHomeService homeService) : UserBaseController
{
    /// <summary>
    /// Returns the aggregated home page data:
    /// total active journals, advisory board member count,
    /// the 6 most recent articles, and 10 random collaborators.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetHomeData))]
    [Route("GetHomeData")]
    public async Task<IActionResult> GetHomeData()
        => ToResult(await homeService.GetHomeDataAsync());
}
