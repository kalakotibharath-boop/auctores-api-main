using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class SubscriberService(ISubscriberRepository repo) : ISubscriberService
{
    public async Task<ServiceResult<IEnumerable<SubscriberDto>>> GetAllAsync()
        => ServiceResult<IEnumerable<SubscriberDto>>.Ok(await repo.GetAllAsync());

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Subscriber not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Subscriber deleted.") : ServiceResult.NotFound("Subscriber not found.");
    }
}
