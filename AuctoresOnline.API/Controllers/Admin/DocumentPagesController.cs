using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Document;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/documents")]
[Authorize]
public class DocumentPagesController(IDocumentService documentService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await documentService.GetAllAsync(page, pageSize, search));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ApiResponse<DocumentDto>>> GetById(long id)
        => ToResult(await documentService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromBody] CreateDocumentRequest req)
    {
        var result = await documentService.CreateAsync(req);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromBody] CreateDocumentRequest req)
        => ToResult(await documentService.UpdateAsync(id, req));

    [HttpPut("{id:long}/status")]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await documentService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await documentService.DeleteAsync(id));

    [HttpPost("upload-ckeditor-image")]
    public async Task<ActionResult<object>> UploadCkEditorImage(IFormFile upload)
    {
        var result = await documentService.UploadCkEditorImageAsync(upload);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(new { uploaded = true, url = result.Data });
    }
}
