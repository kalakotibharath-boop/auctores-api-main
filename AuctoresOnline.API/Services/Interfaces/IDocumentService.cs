using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Document;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IDocumentService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<DocumentDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateDocumentRequest req);
    Task<ServiceResult> UpdateAsync(long id, CreateDocumentRequest req);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload);
}
