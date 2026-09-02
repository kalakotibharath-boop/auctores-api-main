using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class BecomeRequestService(IBecomeRequestRepository repo) : IBecomeRequestService
{
    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<BecomeRequestDto>> GetByIdAsync(long id)
    {
        var item = await repo.GetByIdAsync(id);
        if (item is null) return ServiceResult<BecomeRequestDto>.NotFound("Become request not found.");
        return ServiceResult<BecomeRequestDto>.Ok(item);
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Request deleted.") : ServiceResult.NotFound("Request not found.");
    }
}
