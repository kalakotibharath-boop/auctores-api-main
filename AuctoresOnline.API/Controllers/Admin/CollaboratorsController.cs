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
    [Route(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<IEnumerable<CollaboratorDto>>>> GetAll()
        => ToResult(await collaboratorService.GetAllAsync());

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromForm] CreateCollaboratorRequest req, IFormFile? collaboratorImage)
        => ToResult(await collaboratorService.CreateAsync(req, collaboratorImage));

    [HttpPut]
    [Route(nameof(ToggleStatus))]
    public async Task<ActionResult<ApiResponse>> ToggleStatus(long id, [FromBody] StatusToggleRequest req)
        => ToResult(await collaboratorService.ToggleStatusAsync(id, req.CurrentStatus));

    [HttpDelete]
    [Route(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await collaboratorService.DeleteAsync(id));
}
