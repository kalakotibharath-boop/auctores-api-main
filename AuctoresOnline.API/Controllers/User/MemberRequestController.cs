using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// Public form submission endpoints: manuscript, membership, subscription, and contact.
/// </summary>
[Route("api/user/requests")]
public class MemberRequestController(IUserMemberRequestService requestService) : UserBaseController
{
    /// <summary>
    /// Submits a request to become a journal member or reviewer.
    /// Accepts multipart/form-data so a CV / bio-data file can be attached.
    /// Type: 1 = Member, 2 = Reviewer.
    /// </summary>
    [HttpPost]
    [Route(nameof(BecomeMember))]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> BecomeMember([FromForm] BecomeMemberRequest req)
    {
        var file = Request.Form.Files.GetFile("file");
        return ToResult(await requestService.SubmitMemberRequestAsync(req, file));
    }

    /// <summary>
    /// Subscribes an email address to journal or site updates.
    /// </summary>
    [HttpPost]
    [Route(nameof(Subscribe))]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest req)
        => ToResult(await requestService.SubscribeAsync(req));

    /// <summary>
    /// Submits a manuscript for review.
    /// Accepts multipart/form-data so one or more manuscript files can be attached.
    /// </summary>
    [HttpPost]
    [Route(nameof(SubmitManuscript))]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> SubmitManuscript([FromForm] SubmitManuscriptRequest req)
    {
        var files = Request.Form.Files;
        return ToResult(await requestService.SubmitManuscriptAsync(req, files));
    }

    /// <summary>
    /// Submits a contact us enquiry.
    /// </summary>
    [HttpPost]
    [Route(nameof(ContactUs))]
    public async Task<IActionResult> ContactUs([FromBody] ContactUsRequest req)
        => ToResult(await requestService.AddContactUsAsync(req));
}
