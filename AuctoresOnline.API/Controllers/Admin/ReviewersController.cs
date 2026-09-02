using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Reviewer;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/reviewers")]
[Authorize]
public class ReviewersController(IReviewerService reviewerService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await reviewerService.GetAllAsync(page, pageSize, search));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ApiResponse<ReviewerDto>>> GetById(long id)
        => ToResult(await reviewerService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateReviewerRequest req, IFormFile? image)
    {
        var result = await reviewerService.CreateAsync(req, image);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromForm] CreateReviewerRequest req, IFormFile? image)
        => ToResult(await reviewerService.UpdateAsync(id, req, image));

    [HttpPut("{id:long}/status")]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await reviewerService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await reviewerService.DeleteAsync(id));

    [HttpDelete("{id:long}/image")]
    public async Task<ActionResult<ApiResponse>> DeleteImage(long id)
        => ToResult(await reviewerService.DeleteImageAsync(id));
}
