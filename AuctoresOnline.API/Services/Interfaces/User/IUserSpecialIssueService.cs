using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;

namespace AuctoresOnline.API.Services.Interfaces.User;

public interface IUserSpecialIssueService
{
    Task<ServiceResult<SpecialIssuePublicDetailDto>> GetSpecialIssueBySlugAsync(string slug);
}
