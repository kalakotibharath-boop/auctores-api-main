using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Manuscript;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/manuscript-requests")]
[Authorize]
public class ManuscriptRequestsController(IManuscriptService manuscriptService) : AdminBaseController
{
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await manuscriptService.GetAllAsync(page, pageSize, search));

    [HttpGet(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<ManuscriptRequestDto>>> GetById(long id)
        => ToResult(await manuscriptService.GetByIdAsync(id));

    [HttpPut(nameof(UpdateStatus))]
    public async Task<ActionResult<ApiResponse>> UpdateStatus(long id, [FromBody] ChangeManuscriptStatusRequest req)
        => ToResult(await manuscriptService.UpdateStatusAsync(id, req));

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await manuscriptService.DeleteAsync(id));
}
