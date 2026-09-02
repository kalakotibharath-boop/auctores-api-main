using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IMemberedInService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<MemberedInDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateMemberedInRequest req, IFormFile? image);
    Task<ServiceResult> UpdateAsync(long id, CreateMemberedInRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
}
