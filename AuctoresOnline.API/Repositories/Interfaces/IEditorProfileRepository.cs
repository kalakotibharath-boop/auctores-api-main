using AuctoresOnline.API.Models.EditorProfile;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IEditorProfileRepository : IRepository<EditorProfileDto>
{
    Task<IEnumerable<EditorProfileDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<EditorProfileDto?> GetByIdAsync(long id);
    Task<bool> ExistsByEmailAsync(string email, long? excludeId = null);
    Task<long> CreateAsync(EditorProfileDto profile, string? imageFileName);
    Task<bool> UpdateAsync(long id, EditorProfileDto profile, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<bool> ClearImageAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}
