using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;

namespace AuctoresOnline.API.Services.Interfaces.User;

public interface IUserMemberRequestService
{
    Task<ServiceResult> SubmitMemberRequestAsync(BecomeMemberRequest req, IFormFile? file);
    Task<ServiceResult> SubscribeAsync(SubscribeRequest req);
    Task<ServiceResult> SubmitManuscriptAsync(SubmitManuscriptRequest req, IFormFileCollection? files);
    Task<ServiceResult> AddContactUsAsync(ContactUsRequest req);
}
