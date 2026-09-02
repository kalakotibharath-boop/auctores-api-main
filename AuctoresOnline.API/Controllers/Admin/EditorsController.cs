using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Editor;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/editors")]
[Authorize]
public class EditorsController(IEditorService editorService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await editorService.GetAllAsync(page, pageSize, search));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ApiResponse<EditorDto>>> GetById(long id)
        => ToResult(await editorService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateEditorRequest req, IFormFile? image)
    {
        var result = await editorService.CreateAsync(req, image);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromForm] CreateEditorRequest req, IFormFile? image)
        => ToResult(await editorService.UpdateAsync(id, req, image));

    [HttpPut("{id:long}/status")]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await editorService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await editorService.DeleteAsync(id));

    [HttpDelete("{id:long}/image")]
    public async Task<ActionResult<ApiResponse>> DeleteImage(long id)
        => ToResult(await editorService.DeleteImageAsync(id));
}
