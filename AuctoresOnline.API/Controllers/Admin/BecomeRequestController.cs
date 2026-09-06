using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/become-requests")]
[Authorize]
public class BecomeRequestController(IBecomeRequestService becomeRequestService) : AdminBaseController
{
    [HttpGet]
    [Route(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await becomeRequestService.GetAllAsync(page, pageSize, search));

    [HttpGet]
    [Route(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<BecomeRequestDto>>> GetById(long id)
        => ToResult(await becomeRequestService.GetByIdAsync(id));

    [HttpDelete]
    [Route(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await becomeRequestService.DeleteAsync(id));
}
