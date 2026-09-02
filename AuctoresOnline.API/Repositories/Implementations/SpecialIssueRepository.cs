using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Entities;
using AuctoresOnline.API.Models.SpecialIssue;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class SpecialIssueRepository(ApplicationDbContext context) : Repository<SpecialIssueDto>(context), ISpecialIssueRepository
{
    public async Task<IEnumerable<SpecialIssueDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable
            .GroupJoin(_context.Journals, si => si.JournalId, j => (long)j.JournalId,
                (si, journals) => new { si, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new SpecialIssueDto
                {
                    IssueId = x.si.IssueId, Title = x.si.Title, JournalId = x.si.JournalId,
                    JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    Slug = x.si.Slug, DeadlineDate = x.si.DeadlineDate, IssnNumber = x.si.IssnNumber,
                    CitiesScore = x.si.CitiesScore, ImpactFactor = x.si.ImpactFactor, FileLink = x.si.FileLink,
                    IssueData = x.si.IssueData, IssueDataDisplay = x.si.IssueDataDisplay,
                    Keywords = x.si.Keywords, Benefits = x.si.Benefits,
                    Status = x.si.Status, CreatedDate = x.si.CreatedDate, UpdatedDate = x.si.UpdatedDate
                });

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(si => (si.Title != null && si.Title.Contains(search)) ||
                                      (si.JournalName != null && si.JournalName.Contains(search)));

        return await query.OrderBy(si => si.Title)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(si => si.Title != null && si.Title.Contains(search));
        return await query.CountAsync();
    }

    public async Task<SpecialIssueDto?> GetByIdAsync(long id)
    {
        var issue = await Datatable
            .Where(si => si.IssueId == id)
            .GroupJoin(_context.Journals, si => si.JournalId, j => (long)j.JournalId,
                (si, journals) => new { si, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new SpecialIssueDto
                {
                    IssueId = x.si.IssueId, Title = x.si.Title, JournalId = x.si.JournalId,
                    JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    Slug = x.si.Slug, DeadlineDate = x.si.DeadlineDate, IssnNumber = x.si.IssnNumber,
                    CitiesScore = x.si.CitiesScore, ImpactFactor = x.si.ImpactFactor, FileLink = x.si.FileLink,
                    IssueData = x.si.IssueData, IssueDataDisplay = x.si.IssueDataDisplay,
                    Keywords = x.si.Keywords, Benefits = x.si.Benefits,
                    Status = x.si.Status, CreatedDate = x.si.CreatedDate, UpdatedDate = x.si.UpdatedDate
                })
            .FirstOrDefaultAsync();

        if (issue is null) return null;

        issue.SelectedEditorIds = await _context.SpecialIssueEditors
            .Where(sie => sie.IssueId == id)
            .OrderBy(sie => sie.EditorId)
            .Select(sie => sie.EditorId)
            .ToListAsync();

        return issue;
    }

    public async Task<bool> ExistsByTitleAndJournalAsync(string title, long journalId, long? excludeId = null)
    {
        var query = Datatable.Where(si => si.Title == title && si.JournalId == journalId);
        if (excludeId.HasValue) query = query.Where(si => si.IssueId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(CreateSpecialIssueRequest req, string? fileLink)
    {
        await using var tran = await _context.Database.BeginTransactionAsync();
        try
        {
            var issue = new SpecialIssueDto
            {
                Title = req.Title, JournalId = req.JournalId, Slug = req.Slug,
                DeadlineDate = req.DeadlineDate, IssnNumber = req.IssnNumber, CitiesScore = req.CitiesScore,
                ImpactFactor = req.ImpactFactor, FileLink = fileLink, IssueData = req.IssueData,
                IssueDataDisplay = req.IssueDataDisplay, Keywords = req.Keywords, Benefits = req.Benefits,
                Status = 1, CreatedDate = DateTime.UtcNow
            };
            await _dbSet.AddAsync(issue);
            await _context.SaveChangesAsync();

            if (req.SelectedEditors.Count > 0)
            {
                var editors = req.SelectedEditors.Select(eid => new SpecialIssueEditor
                {
                    IssueId = issue.IssueId, EditorId = eid
                });
                await _context.SpecialIssueEditors.AddRangeAsync(editors);
                await _context.SaveChangesAsync();
            }

            await tran.CommitAsync();
            return issue.IssueId;
        }
        catch
        {
            await tran.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> UpdateAsync(long id, CreateSpecialIssueRequest req, string? fileLink)
    {
        await using var tran = await _context.Database.BeginTransactionAsync();
        try
        {
            var setters = _context.SpecialIssues.Where(si => si.IssueId == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(si => si.Title, req.Title)
                    .SetProperty(si => si.JournalId, req.JournalId)
                    .SetProperty(si => si.Slug, req.Slug)
                    .SetProperty(si => si.DeadlineDate, req.DeadlineDate)
                    .SetProperty(si => si.IssnNumber, req.IssnNumber)
                    .SetProperty(si => si.CitiesScore, req.CitiesScore)
                    .SetProperty(si => si.ImpactFactor, req.ImpactFactor)
                    .SetProperty(si => si.IssueData, req.IssueData)
                    .SetProperty(si => si.IssueDataDisplay, req.IssueDataDisplay)
                    .SetProperty(si => si.Keywords, req.Keywords)
                    .SetProperty(si => si.Benefits, req.Benefits)
                    .SetProperty(si => si.UpdatedDate, DateTime.UtcNow));

            int rows = await setters;
            if (fileLink != null)
                await _context.SpecialIssues.Where(si => si.IssueId == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(si => si.FileLink, fileLink));

            await _context.SpecialIssueEditors.Where(sie => sie.IssueId == id).ExecuteDeleteAsync();

            if (req.SelectedEditors.Count > 0)
            {
                var editors = req.SelectedEditors.Select(eid => new SpecialIssueEditor
                {
                    IssueId = id, EditorId = eid
                });
                await _context.SpecialIssueEditors.AddRangeAsync(editors);
                await _context.SaveChangesAsync();
            }

            await tran.CommitAsync();
            return rows > 0;
        }
        catch
        {
            await tran.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.SpecialIssues
            .Where(si => si.IssueId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(si => si.Status, status)
                .SetProperty(si => si.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await using var tran = await _context.Database.BeginTransactionAsync();
        await _context.SpecialIssueEditors.Where(sie => sie.IssueId == id).ExecuteDeleteAsync();
        int rows = await _context.SpecialIssues.Where(si => si.IssueId == id).ExecuteDeleteAsync();
        await tran.CommitAsync();
        return rows > 0;
    }

    public async Task<string?> GetFileLinkAsync(long id)
        => await Datatable.Where(si => si.IssueId == id).Select(si => si.FileLink).FirstOrDefaultAsync();
}
