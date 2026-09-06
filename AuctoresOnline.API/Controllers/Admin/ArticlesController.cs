using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Article;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/articles")]
[Authorize]
public class ArticlesController(IArticleService articleService) : AdminBaseController
{
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null, [FromQuery] byte? articleType = null)
        => ToResult(await articleService.GetAllAsync(page, pageSize, search, articleType));

    [HttpGet(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<object>>> GetById(long id)
        => ToResult(await articleService.GetByIdAsync(id));

    [HttpPost(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateArticleRequest req, IFormFile? articlePdf)
    {
        var result = await articleService.CreateAsync(req, articlePdf);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut(nameof(Update))]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromForm] CreateArticleRequest req, IFormFile? articlePdf)
        => ToResult(await articleService.UpdateAsync(id, req, articlePdf));

    [HttpPut(nameof(ToggleStatus))]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await articleService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpPut(nameof(ChangeType))]
    public async Task<ActionResult<ApiResponse>> ChangeType(long id, [FromBody] ChangeArticleTypeRequest req)
        => ToResult(await articleService.ChangeTypeAsync(id, req));

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await articleService.DeleteAsync(id));

    [HttpGet(nameof(GetDetails))]
    public async Task<ActionResult<ApiResponse<IEnumerable<ArticleDetailDto>>>> GetDetails(long id)
        => ToResult(await articleService.GetDetailsAsync(id));

    [HttpPost(nameof(SaveDetail))]
    public async Task<ActionResult<ApiResponse<long>>> SaveDetail(long id, [FromBody] SaveArticleDetailRequest req)
        => ToResult(await articleService.SaveDetailAsync(id, req));

    [HttpDelete(nameof(DeleteDetail))]
    public async Task<ActionResult<ApiResponse>> DeleteDetail(long articleId, long detailsId)
        => ToResult(await articleService.DeleteDetailAsync(detailsId));

    [HttpGet(nameof(GetDetail))]
    public async Task<ActionResult<ApiResponse<ArticleDetailDto>>> GetDetail(long detailsId)
        => ToResult(await articleService.GetDetailByIdAsync(detailsId));

    [HttpPost(nameof(UploadCkEditorImage))]
    public async Task<ActionResult<object>> UploadCkEditorImage(IFormFile upload)
    {
        var result = await articleService.UploadCkEditorImageAsync(upload);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(new { uploaded = true, url = result.Data });
    }
}
