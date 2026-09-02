using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuctoresOnline.API.Models.Admin;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/dashboard")]
[Authorize]
public class DashboardController(IAdminService adminService) : AdminBaseController
{
    private int AdminId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<DashboardCountsDto>>> GetCounts()
        => ToResult(await adminService.GetDashboardCountsAsync());

    [HttpGet("profile")]
    public async Task<ActionResult<ApiResponse<AdminDto>>> GetProfile()
        => ToResult(await adminService.GetProfileAsync(AdminId));

    [HttpPut("profile")]
    public async Task<ActionResult<ApiResponse>> UpdateProfile([FromBody] UpdateProfileRequest req)
        => ToResult(await adminService.UpdateProfileAsync(AdminId, req));

    [HttpPut("change-password")]
    public async Task<ActionResult<ApiResponse>> ChangePassword([FromBody] ChangePasswordRequest req)
        => ToResult(await adminService.ChangePasswordAsync(AdminId, req));
}
