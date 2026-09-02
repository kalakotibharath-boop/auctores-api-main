using AuctoresOnline.API.Models.Admin;
using AuctoresOnline.API.Models.Common;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IAdminService
{
    Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest req);
    Task<ServiceResult<DashboardCountsDto>> GetDashboardCountsAsync();
    Task<ServiceResult<AdminDto>> GetProfileAsync(int adminId);
    Task<ServiceResult> UpdateProfileAsync(int adminId, UpdateProfileRequest req);
    Task<ServiceResult> ChangePasswordAsync(int adminId, ChangePasswordRequest req);
}
