using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Author;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/authors")]
[Authorize]
public class AuthorsController(IAuthorService authorService) : AdminBaseController
{
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await authorService.GetAllAsync(page, pageSize, search));

    [HttpGet(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<AuthorDto>>> GetById(long id)
        => ToResult(await authorService.GetByIdAsync(id));

    [HttpPost(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateAuthorRequest req, IFormFile? image)
    {
        var result = await authorService.CreateAsync(req, image);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut(nameof(Update))]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromForm] CreateAuthorRequest req, IFormFile? image)
        => ToResult(await authorService.UpdateAsync(id, req, image));

    [HttpPut(nameof(ToggleStatus))]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await authorService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await authorService.DeleteAsync(id));

    [HttpDelete(nameof(DeleteImage))]
    public async Task<ActionResult<ApiResponse>> DeleteImage(long id)
        => ToResult(await authorService.DeleteImageAsync(id));
}
