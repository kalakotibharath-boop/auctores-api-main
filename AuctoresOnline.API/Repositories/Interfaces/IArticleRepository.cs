using AuctoresOnline.API.Models.Article;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IArticleRepository : IRepository<ArticleDto>
{
    Task<IEnumerable<ArticleDto>> GetAllAsync(int page, int pageSize, string? search, byte? articleType);
    Task<int> GetTotalCountAsync(string? search, byte? articleType);
    Task<ArticleDto?> GetByIdAsync(long id);
    Task<bool> ExistsByNameAndJournalAsync(string name, int journalId, long? excludeId = null);
    Task<long> CreateAsync(ArticleDto article, string pdfFileName);
    Task<bool> UpdateAsync(long id, ArticleDto article, string? newPdfFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> UpdateTypeAsync(long id, byte type);
    Task<bool> DeleteAsync(long id);
    Task ReplaceAuthorsAsync(long articleId, List<ArticleAuthorRequest> authors);
    Task ReplaceReferencesAsync(long articleId, List<ArticleReferenceRequest> references);
    Task<IEnumerable<ArticleDetailDto>> GetDetailsAsync(long articleId);
    Task<ArticleDetailDto?> GetDetailByIdAsync(long detailsId);
    Task<long> SaveDetailAsync(long articleId, SaveArticleDetailRequest request);
    Task<bool> DeleteDetailAsync(long detailsId);
    Task UpdateCkEditorImagesAsync(long articleId, string dataOf);
}
