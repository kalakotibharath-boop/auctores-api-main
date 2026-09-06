using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/article-processing-charges")]
[Authorize]
public class ArticleProcessingChargesController(IApcService apcService) : AdminBaseController
{
    [HttpGet]
    [Route(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<IEnumerable<ApcDto>>>> GetAll()
        => ToResult(await apcService.GetAllAsync());

    [HttpGet]
    [Route(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<ApcDto>>> GetById(long id)
        => ToResult(await apcService.GetByIdAsync(id));

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<ActionResult<ApiResponse<long>>> Create([FromBody] CreateApcRequest req)
    {
        var result = await apcService.CreateAsync(req);
        if (!result.Success) return ToResult(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, ApiResponse<long>.Ok(result.Data, result.Message));
    }

    [HttpPut]
    [Route(nameof(Update))]
    public async Task<ActionResult<ApiResponse>> Update(long id, [FromBody] CreateApcRequest req)
        => ToResult(await apcService.UpdateAsync(id, req));

    [HttpDelete]
    [Route(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await apcService.DeleteAsync(id));
}
