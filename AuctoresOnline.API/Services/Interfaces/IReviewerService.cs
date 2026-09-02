using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Reviewer;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IReviewerService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<ReviewerDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateReviewerRequest req, IFormFile? image);
    Task<ServiceResult> UpdateAsync(long id, CreateReviewerRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult> DeleteImageAsync(long id);
}
