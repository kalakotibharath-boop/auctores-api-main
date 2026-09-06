using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/membered-in")]
[Authorize]
public class MemberedInController(IMemberedInService memberedInService) : AdminBaseController
{
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await memberedInService.GetAllAsync(page, pageSize, search));

    [HttpGet(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<MemberedInDto>>> GetById(long id)
        => ToResult(await memberedInService.GetByIdAsync(id));

    [HttpPost(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateMemberedInRequest req, IFormFile? image)
    {
        var result = await memberedInService.CreateAsync(req, image);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut(nameof(Update))]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromForm] CreateMemberedInRequest req, IFormFile? image)
        => ToResult(await memberedInService.UpdateAsync(id, req, image));

    [HttpPut(nameof(ToggleStatus))]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await memberedInService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await memberedInService.DeleteAsync(id));
}
