using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/collaborators")]
[Authorize]
public class CollaboratorsController(ICollaboratorService collaboratorService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CollaboratorDto>>>> GetAll()
        => ToResult(await collaboratorService.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateCollaboratorRequest req, IFormFile? collaboratorImage)
        => ToResult(await collaboratorService.CreateAsync(req, collaboratorImage));

    [HttpPut("{id:long}/status")]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await collaboratorService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await collaboratorService.DeleteAsync(id));
}
