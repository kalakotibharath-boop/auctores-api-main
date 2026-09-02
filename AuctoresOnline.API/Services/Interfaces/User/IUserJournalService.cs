using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;

namespace AuctoresOnline.API.Services.Interfaces.User;

public interface IUserJournalService
{
    Task<ServiceResult<JournalListingDto>> GetJournalListAsync();
    Task<ServiceResult<JournalPublicDetailDto>> GetJournalDetailAsync(string seoName);
    Task<ServiceResult<EditorialBoardDto>> GetEditorialBoardAsync(string seoName);
    Task<ServiceResult<EditorProfilesPublicDto>> GetEditorProfilesAsync(string seoName);
    Task<ServiceResult<IEnumerable<ArticleListItemDto>>> GetArticlesInPressAsync(string seoName);
    Task<ServiceResult<IEnumerable<ArticleListItemDto>>> GetCurrentIssuesAsync(string seoName);
    Task<ServiceResult<ArchiveDataDto>> GetArchivesAsync(string seoName);
    Task<ServiceResult<IEnumerable<ArticleListItemDto>>> GetArchiveArticlesAsync(string seoName, int volumeNo, int issueNo);
    Task<ServiceResult<IEnumerable<PubmedIndexPublicDto>>> GetPubmedIndexAsync(string seoName);
    Task<ServiceResult<IEnumerable<AbstractingIndexingPublicDto>>> GetAbstractIndexingAsync(string seoName);
    Task<ServiceResult<IEnumerable<SpecialIssueListItemDto>>> GetSpecialIssuesAsync(string seoName);
    Task<ServiceResult<JournalPublicDetailDto>> GetAboutJournalAsync(string seoName);
    Task<ServiceResult<IEnumerable<JournalSubmitOptionDto>>> GetJournalsForSelectAsync();
    Task<ServiceResult<IEnumerable<ApcPublicDto>>> GetAllArticleProcessingChargesAsync();
}
