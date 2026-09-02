using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Editor;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IEditorService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<EditorDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateEditorRequest req, IFormFile? image);
    Task<ServiceResult> UpdateAsync(long id, CreateEditorRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult> DeleteImageAsync(long id);
}
