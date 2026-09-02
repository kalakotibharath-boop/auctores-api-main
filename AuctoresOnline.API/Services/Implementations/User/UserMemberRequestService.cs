using AuctoresOnline.API.Data;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Models.Manuscript;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserMemberRequestService(
    ApplicationDbContext context,
    FileUploadHelper fileHelper) : IUserMemberRequestService
{
    // ── Register as member or reviewer ────────────────────────────────────────

    public async Task<ServiceResult> SubmitMemberRequestAsync(BecomeMemberRequest req, IFormFile? file)
    {
        string? uploadedFile = null;

        if (file is not null && file.Length > 0)
        {
            try
            {
                uploadedFile = await fileHelper.UploadFileAsync(file, "editor_profiles");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"File upload failed: {ex.Message}");
            }
        }

        var entry = new BecomeRequestDto
        {
            Name = req.Name.Trim(),
            Email = req.Email.Trim(),
            PhoneNumber = req.PhoneNumber?.Trim(),
            Designation = req.Designation?.Trim(),
            Country = req.Country?.Trim(),
            BioData = req.BioData?.Trim(),
            JournalId = req.JournalId,
            Type = req.Type,
            Files = uploadedFile,
            CreatedDate = DateTime.UtcNow
        };

        await context.BecomeRequests.AddAsync(entry);
        await context.SaveChangesAsync();

        return ServiceResult.Ok("Request submitted successfully.");
    }

    // ── Newsletter / journal subscription ─────────────────────────────────────

    public async Task<ServiceResult> SubscribeAsync(SubscribeRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Email))
            return ServiceResult.Fail("Email address is required.");

        var subscription = new SubscriberDto
        {
            EmailId = req.Email.Trim(),
            SubscribedFor = req.SubscribedFor?.Trim(),
            SubscribedForId = req.SubscribedForId,
            SubscriptionStatus = 1,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await context.SubscriptionList.AddAsync(subscription);
        await context.SaveChangesAsync();

        return ServiceResult.Ok("Subscribed successfully.");
    }

    // ── Submit manuscript ─────────────────────────────────────────────────────

    public async Task<ServiceResult> SubmitManuscriptAsync(SubmitManuscriptRequest req, IFormFileCollection? files)
    {
        var uploadedFiles = new List<string>();

        if (files is not null)
        {
            foreach (var file in files)
            {
                if (file.Length == 0) continue;
                try
                {
                    var uploaded = await fileHelper.UploadFileAsync(file, "manuscripts");
                    uploadedFiles.Add(uploaded);
                }
                catch (Exception ex)
                {
                    return ServiceResult.Fail($"File upload failed: {ex.Message}");
                }
            }
        }

        var manuscript = new ManuscriptRequestDto
        {
            FirstName = req.AFname?.Trim(),
            LastName = req.ALname?.Trim(),
            Email = req.AEmail?.Trim(),
            Phone = req.APhone?.Trim(),
            Country = req.ACountry?.Trim(),
            Orcid = req.AOrcid?.Trim() ?? "",
            Address = req.AAddress?.Trim(),
            ManuscriptTitle = req.MTitle?.Trim(),
            JournalId = req.JournalId,
            ArticleType = req.MArticleType?.Trim(),
            Abstract = req.MAbstract?.Trim(),
            Keywords = req.MKeywords?.Trim(),
            CoverLetter = req.MCoverLetter?.Trim(),
            Files = uploadedFiles.Count > 0 ? string.Join(",", uploadedFiles) : null,
            Status = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await context.ManuscriptRequests.AddAsync(manuscript);
        await context.SaveChangesAsync();

        return ServiceResult.Ok("Manuscript submitted successfully. Our editorial team will contact you shortly.");
    }

    // ── Contact us ────────────────────────────────────────────────────────────

    public async Task<ServiceResult> AddContactUsAsync(ContactUsRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Name) || string.IsNullOrWhiteSpace(req.Email))
            return ServiceResult.Fail("Name and email are required.");

        var contact = new ContactDto
        {
            ContactName = req.Name.Trim(),
            ContactEmail = req.Email.Trim(),
            ContactPhone = req.Phone?.Trim(),
            ContactSubject = req.Subject?.Trim(),
            ContactMessage = req.Message?.Trim(),
            ContactStatus = "Pending",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await context.Contacts.AddAsync(contact);
        await context.SaveChangesAsync();

        return ServiceResult.Ok("Thank you for reaching out. Our team will contact you shortly.");
    }
}
