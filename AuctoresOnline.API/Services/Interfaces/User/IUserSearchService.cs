using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;

namespace AuctoresOnline.API.Services.Interfaces.User;

public interface IUserSearchService
{
    Task<ServiceResult<IEnumerable<ArticleListItemDto>>> SearchArticlesAsync(string keyword);
}
