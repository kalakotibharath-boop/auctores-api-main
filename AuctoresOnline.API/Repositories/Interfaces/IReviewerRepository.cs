using AuctoresOnline.API.Models.Reviewer;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IReviewerRepository : IRepository<ReviewerDto>
{
    Task<IEnumerable<ReviewerDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<ReviewerDto?> GetByIdAsync(long id);
    Task<bool> ExistsByEmailAsync(string email, long? excludeId = null);
    Task<long> CreateAsync(ReviewerDto reviewer, string? imageFileName);
    Task<bool> UpdateAsync(long id, ReviewerDto reviewer, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<bool> ClearImageAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}
