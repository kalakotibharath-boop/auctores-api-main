using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.SpecialIssue;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class SpecialIssueService(ISpecialIssueRepository repo, FileUploadHelper fileHelper) : ISpecialIssueService
{
    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<SpecialIssueDto>> GetByIdAsync(long id)
    {
        var issue = await repo.GetByIdAsync(id);
        if (issue is null) return ServiceResult<SpecialIssueDto>.NotFound("Special issue not found.");
        return ServiceResult<SpecialIssueDto>.Ok(issue);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateSpecialIssueRequest req, IFormFile? pdf)
    {
        if (!string.IsNullOrEmpty(req.Title) && await repo.ExistsByTitleAndJournalAsync(req.Title, req.JournalId))
            return ServiceResult<long>.Conflict("A special issue with this title already exists for this journal.");

        string? fileLink = null;
        if (pdf != null) fileLink = await fileHelper.UploadPdfAsync(pdf, "special_issues");
        var newId = await repo.CreateAsync(req, fileLink);
        return ServiceResult<long>.Ok(newId, "Special issue created.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateSpecialIssueRequest req, IFormFile? pdf)
    {
        if (!string.IsNullOrEmpty(req.Title) && await repo.ExistsByTitleAndJournalAsync(req.Title, req.JournalId, id))
            return ServiceResult.Conflict("A special issue with this title already exists.");

        string? fileLink = null;
        if (pdf != null)
        {
            var old = await repo.GetFileLinkAsync(id);
            if (!string.IsNullOrEmpty(old)) fileHelper.DeleteFile("special_issues", old);
            fileLink = await fileHelper.UploadPdfAsync(pdf, "special_issues");
        }

        return await repo.UpdateAsync(id, req, fileLink)
            ? ServiceResult.Ok("Special issue updated.") : ServiceResult.NotFound("Not found.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Special issue not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Special issue deleted.") : ServiceResult.NotFound("Not found.");
    }

    public async Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload)
    {
        var fileName = await fileHelper.UploadImageAsync(upload, "special_issues");
        return ServiceResult<string>.Ok(fileHelper.GetFileUrl("special_issues", fileName));
    }
}
