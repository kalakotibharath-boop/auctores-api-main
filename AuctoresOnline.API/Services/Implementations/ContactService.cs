using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class ContactService(IContactRepository repo) : IContactService
{
    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<ContactDto>> GetByIdAsync(long id)
    {
        var contact = await repo.GetByIdAsync(id);
        if (contact is null) return ServiceResult<ContactDto>.NotFound("Contact not found.");
        return ServiceResult<ContactDto>.Ok(contact);
    }

    public async Task<ServiceResult> UpdateStatusAsync(long id, UpdateContactStatusRequest req)
    {
        return await repo.UpdateStatusAsync(id, req.Status)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Contact not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Contact deleted.") : ServiceResult.NotFound("Contact not found.");
    }
}
