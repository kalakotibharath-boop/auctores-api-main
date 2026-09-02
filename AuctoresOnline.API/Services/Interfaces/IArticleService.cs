using AuctoresOnline.API.Models.Article;
using AuctoresOnline.API.Models.Common;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IArticleService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search, byte? articleType);
    Task<ServiceResult<object>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateArticleRequest req, IFormFile? pdf);
    Task<ServiceResult> UpdateAsync(long id, CreateArticleRequest req, IFormFile? pdf);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> ChangeTypeAsync(long id, ChangeArticleTypeRequest req);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult<IEnumerable<ArticleDetailDto>>> GetDetailsAsync(long articleId);
    Task<ServiceResult<long>> SaveDetailAsync(long articleId, SaveArticleDetailRequest req);
    Task<ServiceResult> DeleteDetailAsync(long detailsId);
    Task<ServiceResult<ArticleDetailDto>> GetDetailByIdAsync(long detailsId);
    Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload);
}
