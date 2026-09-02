using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Admin;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/auth")]
public class AuthController(IAdminService adminService) : AdminBaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest req)
    {
        var result = await adminService.LoginAsync(req);
        return ToResult(result);
    }
}
