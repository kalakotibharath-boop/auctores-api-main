using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IBannerService
{
    Task<ServiceResult<IEnumerable<BannerDto>>> GetAllAsync();
    Task<ServiceResult<long>> CreateAsync(CreateBannerRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
}
