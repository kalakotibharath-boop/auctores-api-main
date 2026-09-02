using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/testimonials")]
[Authorize]
public class TestimonialsController(ITestimonialService testimonialService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<TestimonialDto>>>> GetAll()
        => ToResult(await testimonialService.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateTestimonialRequest req, IFormFile? testimonialImage)
        => ToResult(await testimonialService.CreateAsync(req, testimonialImage));

    [HttpPut("{id:long}/status")]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await testimonialService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await testimonialService.DeleteAsync(id));
}
