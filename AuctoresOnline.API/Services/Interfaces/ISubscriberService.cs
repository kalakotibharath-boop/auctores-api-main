using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface ISubscriberService
{
    Task<ServiceResult<IEnumerable<SubscriberDto>>> GetAllAsync();
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
}
