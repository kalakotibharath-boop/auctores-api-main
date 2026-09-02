using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Journal;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/journals")]
[Authorize]
public class JournalsController(IJournalService journalService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await journalService.GetAllAsync(page, pageSize, search));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<JournalDetailDto>>> GetById(int id)
        => ToResult(await journalService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromForm] CreateJournalRequest req,
        IFormFile? journalImage, IFormFile? indexingImage, IFormFile? crossrefImage)
    {
        var result = await journalService.CreateAsync(req, journalImage, indexingImage, crossrefImage);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<int>.Ok(result.Data, result.Message));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Update(int id, [FromForm] CreateJournalRequest req,
        IFormFile? journalImage, IFormFile? indexingImage, IFormFile? crossrefImage)
        => ToResult(await journalService.UpdateAsync(id, req, journalImage, indexingImage, crossrefImage));

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(int id, [FromBody] StatusToggleRequest req)
        => ToResult(await journalService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
        => ToResult(await journalService.DeleteAsync(id));

    [HttpPost("upload-ckeditor-image")]
    public async Task<ActionResult<object>> UploadCkEditorImage(IFormFile upload)
    {
        var result = await journalService.UploadCkEditorImageAsync(upload);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(new { uploaded = true, url = result.Data });
    }
}
