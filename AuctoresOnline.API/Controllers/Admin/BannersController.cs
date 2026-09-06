using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/banners")]
[Authorize]
public class BannersController(IBannerService bannerService) : AdminBaseController
{
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<IEnumerable<BannerDto>>>> GetAll()
        => ToResult(await bannerService.GetAllAsync());

    [HttpPost(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateBannerRequest req, IFormFile? bannerImage)
        => ToResult(await bannerService.CreateAsync(req, bannerImage));

    [HttpPut(nameof(ToggleStatus))]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await bannerService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await bannerService.DeleteAsync(id));
}
