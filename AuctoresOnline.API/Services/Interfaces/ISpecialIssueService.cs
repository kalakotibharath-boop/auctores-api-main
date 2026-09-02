using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.SpecialIssue;

namespace AuctoresOnline.API.Services.Interfaces;

public interface ISpecialIssueService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<SpecialIssueDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateSpecialIssueRequest req, IFormFile? pdf);
    Task<ServiceResult> UpdateAsync(long id, CreateSpecialIssueRequest req, IFormFile? pdf);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload);
}
