using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.EditorProfile;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/editor-profiles")]
[Authorize]
public class EditorProfilesController(IEditorProfileService editorProfileService) : AdminBaseController
{
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await editorProfileService.GetAllAsync(page, pageSize, search));

    [HttpGet(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<EditorProfileDto>>> GetById(long id)
        => ToResult(await editorProfileService.GetByIdAsync(id));

    [HttpPost(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateEditorProfileRequest req, IFormFile? image)
    {
        var result = await editorProfileService.CreateAsync(req, image);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut(nameof(Update))]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromForm] CreateEditorProfileRequest req, IFormFile? image)
        => ToResult(await editorProfileService.UpdateAsync(id, req, image));

    [HttpPut(nameof(ToggleStatus))]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await editorProfileService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await editorProfileService.DeleteAsync(id));

    [HttpDelete(nameof(DeleteImage))]
    public async Task<ActionResult<ApiResponse>> DeleteImage(long id)
        => ToResult(await editorProfileService.DeleteImageAsync(id));
}
