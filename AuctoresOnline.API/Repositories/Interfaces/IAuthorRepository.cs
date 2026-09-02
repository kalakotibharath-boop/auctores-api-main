using AuctoresOnline.API.Models.Author;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IAuthorRepository : IRepository<AuthorDto>
{
    Task<IEnumerable<AuthorDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<AuthorDto?> GetByIdAsync(long id);
    Task<bool> ExistsByEmailAsync(string email, long? excludeId = null);
    Task<long> CreateAsync(AuthorDto author, string? imageFileName);
    Task<bool> UpdateAsync(long id, AuthorDto author, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<bool> ClearImageAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}
