using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IBecomeRequestService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<BecomeRequestDto>> GetByIdAsync(long id);
    Task<ServiceResult> DeleteAsync(long id);
}
