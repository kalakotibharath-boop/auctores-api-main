using AuctoresOnline.API.Models.Editor;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IEditorRepository : IRepository<EditorDto>
{
    Task<IEnumerable<EditorDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<EditorDto?> GetByIdAsync(long id);
    Task<bool> ExistsByEmailAsync(string email, long? excludeId = null);
    Task<long> CreateAsync(EditorDto editor, string? imageFileName);
    Task<bool> UpdateAsync(long id, EditorDto editor, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<bool> ClearImageAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}
