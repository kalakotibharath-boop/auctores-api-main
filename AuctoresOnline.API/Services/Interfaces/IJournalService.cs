using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Journal;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IJournalService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<JournalDetailDto>> GetByIdAsync(int id);
    Task<ServiceResult<int>> CreateAsync(CreateJournalRequest req, IFormFile? journalImage, IFormFile? indexingImage, IFormFile? crossrefImage);
    Task<ServiceResult> UpdateAsync(int id, CreateJournalRequest req, IFormFile? journalImage, IFormFile? indexingImage, IFormFile? crossrefImage);
    Task<ServiceResult> ToggleStatusAsync(int id, int currentStatus);
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload);
}
