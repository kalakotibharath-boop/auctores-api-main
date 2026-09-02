using AuctoresOnline.API.Models.Author;
using AuctoresOnline.API.Models.Common;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IAuthorService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<AuthorDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateAuthorRequest req, IFormFile? image);
    Task<ServiceResult> UpdateAsync(long id, CreateAuthorRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult> DeleteImageAsync(long id);
}
