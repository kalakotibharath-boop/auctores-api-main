using AuctoresOnline.API.Models.Admin;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IAdminRepository : IRepository<AdminDto>
{
    Task<AdminDto?> GetByUsernameAsync(string username);
    Task<AdminDto?> GetByIdAsync(int id);
    Task<string?> GetPasswordAsync(int id);
    Task<bool> UpdateProfileAsync(int id, string name, string email, string mobile);
    Task<bool> UpdatePasswordAsync(int id, string passwordHash);
    Task<string?> GetUsernamePasswordAsync(string username);
    Task<DashboardCountsDto> GetDashboardCountsAsync();
}
