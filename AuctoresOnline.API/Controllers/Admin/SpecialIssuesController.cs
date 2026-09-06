using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.SpecialIssue;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/special-issues")]
[Authorize]
public class SpecialIssuesController(ISpecialIssueService specialIssueService) : AdminBaseController
{
    [HttpGet]
    [Route(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await specialIssueService.GetAllAsync(page, pageSize, search));

    [HttpGet]
    [Route(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<SpecialIssueDto>>> GetById(long id)
        => ToResult(await specialIssueService.GetByIdAsync(id));

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateSpecialIssueRequest req, IFormFile? pdfFile)
    {
        var result = await specialIssueService.CreateAsync(req, pdfFile);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut]
    [Route(nameof(Update))]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromForm] CreateSpecialIssueRequest req, IFormFile? pdfFile)
        => ToResult(await specialIssueService.UpdateAsync(id, req, pdfFile));

    [HttpPut]
    [Route(nameof(ToggleStatus))]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await specialIssueService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete]
    [Route(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await specialIssueService.DeleteAsync(id));

    [HttpPost]
    [Route(nameof(UploadCkEditorImage))]
    public async Task<ActionResult<object>> UploadCkEditorImage(IFormFile upload)
    {
        var result = await specialIssueService.UploadCkEditorImageAsync(upload);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(new { uploaded = true, url = result.Data });
    }
}
