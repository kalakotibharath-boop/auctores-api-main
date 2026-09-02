using AuctoresOnline.API.Models.Journal;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IJournalRepository : IRepository<JournalDto>
{
    Task<IEnumerable<JournalDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<JournalDto?> GetByIdAsync(int id);
    Task<JournalDetailDto?> GetDetailsByIdAsync(int id);
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
    Task<int> CreateAsync(JournalDto journal, string? journalImage, string? indexingImage, string? crossrefImage);
    Task<bool> UpdateAsync(int id, JournalDto journal, string? journalImage, string? indexingImage, string? crossrefImage);
    Task<bool> UpdateStatusAsync(int id, int status);
    Task<bool> DeleteAsync(int id);
    Task ReplaceEditorialBoardAsync(int journalId, Dictionary<string, List<long>> roleEditors);
    Task ReplaceSectionsAsync(int journalId, List<string> sections);
}
