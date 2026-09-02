using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/pubmed-index")]
[Authorize]
public class PubmedIndexController(IPubmedIndexService pubmedService) : AdminBaseController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PubmedIndexDto>>>> GetAll()
        => ToResult(await pubmedService.GetAllAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ApiResponse<PubmedIndexDto>>> GetById(long id)
        => ToResult(await pubmedService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromBody] CreatePubmedIndexRequest req)
    {
        var result = await pubmedService.CreateAsync(req);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromBody] CreatePubmedIndexRequest req)
        => ToResult(await pubmedService.UpdateAsync(id, req));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await pubmedService.DeleteAsync(id));
}
