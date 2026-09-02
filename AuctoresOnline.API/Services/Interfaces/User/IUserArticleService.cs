using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;

namespace AuctoresOnline.API.Services.Interfaces.User;

public interface IUserArticleService
{
    Task<ServiceResult<ArticlePublicDetailDto>> GetArticleBySlugAsync(string seoName);
    Task<ServiceResult> IncrementDownloadCountAsync(long articleId);
    Task<ServiceResult> IncrementViewCountAsync(long articleId);
}
