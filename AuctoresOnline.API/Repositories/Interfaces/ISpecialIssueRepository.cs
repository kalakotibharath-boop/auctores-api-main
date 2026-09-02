using AuctoresOnline.API.Models.SpecialIssue;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface ISpecialIssueRepository : IRepository<SpecialIssueDto>
{
    Task<IEnumerable<SpecialIssueDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<SpecialIssueDto?> GetByIdAsync(long id);
    Task<bool> ExistsByTitleAndJournalAsync(string title, long journalId, long? excludeId = null);
    Task<long> CreateAsync(CreateSpecialIssueRequest request, string? fileLink);
    Task<bool> UpdateAsync(long id, CreateSpecialIssueRequest request, string? fileLink);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<string?> GetFileLinkAsync(long id);
}
