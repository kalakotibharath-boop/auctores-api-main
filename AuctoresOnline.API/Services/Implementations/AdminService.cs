using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Admin;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class AdminService(IAdminRepository repo, JwtHelper jwt) : IAdminService
{
    public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest req)
    {
        var admin = await repo.GetByUsernameAsync(req.Username);
        if (admin is null) return ServiceResult<LoginResponse>.Unauthorized("Invalid username or password.");

        var hash = await repo.GetPasswordAsync(admin.Id);
        if (hash is null || !Md5Helper.Verify(req.Password, hash))
            return ServiceResult<LoginResponse>.Unauthorized("Invalid username or password.");

        var token = jwt.GenerateToken(admin.Id, admin.Name, admin.Email, admin.RoleType);
        return ServiceResult<LoginResponse>.Ok(new LoginResponse(token, admin));
    }

    public async Task<ServiceResult<DashboardCountsDto>> GetDashboardCountsAsync()
    {
        var counts = await repo.GetDashboardCountsAsync();
        return ServiceResult<DashboardCountsDto>.Ok(counts);
    }

    public async Task<ServiceResult<AdminDto>> GetProfileAsync(int adminId)
    {
        var admin = await repo.GetByIdAsync(adminId);
        if (admin is null) return ServiceResult<AdminDto>.NotFound("Admin not found.");
        return ServiceResult<AdminDto>.Ok(admin);
    }

    public async Task<ServiceResult> UpdateProfileAsync(int adminId, UpdateProfileRequest req)
    {
        var updated = await repo.UpdateProfileAsync(adminId, req.Name, req.Email, req.Mobile);
        return updated ? ServiceResult.Ok("Profile updated successfully.") : ServiceResult.Fail("Update failed.");
    }

    public async Task<ServiceResult> ChangePasswordAsync(int adminId, ChangePasswordRequest req)
    {
        if (req.NewPassword != req.ConfirmPassword)
            return ServiceResult.Fail("New password and confirm password do not match.");

        var hash = await repo.GetPasswordAsync(adminId);
        if (hash is null || !Md5Helper.Verify(req.CurrentPassword, hash))
            return ServiceResult.Fail("Current password is incorrect.");

        var updated = await repo.UpdatePasswordAsync(adminId, Md5Helper.Hash(req.NewPassword));
        return updated ? ServiceResult.Ok("Password changed successfully.") : ServiceResult.Fail("Password change failed.");
    }
}
