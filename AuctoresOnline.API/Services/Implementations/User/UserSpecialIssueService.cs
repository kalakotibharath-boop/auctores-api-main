using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserSpecialIssueService(ApplicationDbContext context) : IUserSpecialIssueService
{
    public async Task<ServiceResult<SpecialIssuePublicDetailDto>> GetSpecialIssueBySlugAsync(string slug)
    {
        var issue = await context.SpecialIssues
            .Where(si => si.Status == 1 && si.Slug == slug)
            .Join(context.Journals, si => si.JournalId, j => j.JournalId,
                (si, j) => new { si, JournalName = j.JournalName, JournalSeoName = j.JournalSeoName })
            .FirstOrDefaultAsync();

        if (issue is null)
            return ServiceResult<SpecialIssuePublicDetailDto>.NotFound("Special issue not found.");

        // Increment view count
        await context.SpecialIssues
            .Where(si => si.IssueId == issue.si.IssueId)
            .ExecuteUpdateAsync(s => s.SetProperty(si => si.ViewCount, si => si.ViewCount + 1));

        // Load associated editors
        var editors = await context.SpecialIssueEditors
            .Where(se => se.IssueId == issue.si.IssueId)
            .Join(context.EditorProfiles, se => se.EditorId, ep => ep.EditorId,
                (se, ep) => new SpecialIssueEditorPublicDto
                {
                    EditorId = ep.EditorId,
                    EditorName = ep.EditorName,
                    EditorDesignation = ep.EditorDesignation,
                    EditorImage = ep.EditorImage
                })
            .ToListAsync();

        var result = new SpecialIssuePublicDetailDto
        {
            IssueId = issue.si.IssueId,
            Title = issue.si.Title,
            JournalId = issue.si.JournalId,
            JournalName = issue.JournalName,
            JournalSeoName = issue.JournalSeoName,
            Slug = issue.si.Slug,
            DeadlineDate = issue.si.DeadlineDate,
            ViewCount = issue.si.ViewCount + 1,
            IssnNumber = issue.si.IssnNumber,
            IssueDataDisplay = issue.si.IssueDataDisplay,
            Keywords = issue.si.Keywords,
            Benefits = issue.si.Benefits,
            FileLink = issue.si.FileLink,
            Editors = editors
        };

        return ServiceResult<SpecialIssuePublicDetailDto>.Ok(result);
    }
}
