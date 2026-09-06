using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Controllers.Admin;

[ApiController]
[Route("api/admin/contacts")]
[Authorize]
public class ContactsController(IContactService contactService) : AdminBaseController
{
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<ApiResponse<object>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => ToResult(await contactService.GetAllAsync(page, pageSize, search));

    [HttpGet(nameof(GetById))]
    public async Task<ActionResult<ApiResponse<ContactDto>>> GetById(long id)
        => ToResult(await contactService.GetByIdAsync(id));

    [HttpPut(nameof(UpdateStatus))]
    public async Task<ActionResult<ApiResponse>> UpdateStatus(long id, [FromBody] UpdateContactStatusRequest req)
        => ToResult(await contactService.UpdateStatusAsync(id, req));

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<ApiResponse>> Delete(long id)
        => ToResult(await contactService.DeleteAsync(id));
}
