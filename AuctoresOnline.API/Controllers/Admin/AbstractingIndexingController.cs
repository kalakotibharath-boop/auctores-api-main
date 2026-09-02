using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/abstracting-indexing")]
[Authorize]
public class AbstractingIndexingController(IAbstractingIndexingService abstractingService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<AbstractingIndexingDto>>>> GetAll()
        => ToResult(await abstractingService.GetAllAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ApiResponse<AbstractingIndexingDto>>> GetById(long id)
        => ToResult(await abstractingService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromBody] CreateAbstractingIndexingRequest req)
    {
        var result = await abstractingService.CreateAsync(req);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromBody] CreateAbstractingIndexingRequest req)
        => ToResult(await abstractingService.UpdateAsync(id, req));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await abstractingService.DeleteAsync(id));
}
