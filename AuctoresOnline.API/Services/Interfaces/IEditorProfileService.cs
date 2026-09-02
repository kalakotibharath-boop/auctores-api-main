using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.EditorProfile;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IEditorProfileService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<EditorProfileDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateEditorProfileRequest req, IFormFile? image);
    Task<ServiceResult> UpdateAsync(long id, CreateEditorProfileRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult> DeleteImageAsync(long id);
}
