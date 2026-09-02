using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Journal;
using AuctoresOnline.API.Entities;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class JournalRepository(ApplicationDbContext context) : Repository<JournalDto>(context), IJournalRepository
{
    public async Task<IEnumerable<JournalDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(j => j.JournalName.Contains(search) ||
                                     (j.JournalSeoName != null && j.JournalSeoName.Contains(search)));
        return await query
            .OrderBy(j => j.JournalName)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(j => j.JournalName.Contains(search));
        return await query.CountAsync();
    }

    public async Task<JournalDto?> GetByIdAsync(int id)
        => await Datatable.Where(j => j.JournalId == id).FirstOrDefaultAsync();

    public async Task<JournalDetailDto?> GetDetailsByIdAsync(int id)
    {
        var journal = await Datatable.Where(j => j.JournalId == id)
            .Select(j => new JournalDetailDto
            {
                JournalId = j.JournalId, JournalName = j.JournalName, JournalDoi = j.JournalDoi,
                JournalAcceptanceRate = j.JournalAcceptanceRate, JournalDecision = j.JournalDecision,
                JournalAcceptancePublic = j.JournalAcceptancePublic, JournalCitescore1 = j.JournalCitescore1,
                JournalImpact = j.JournalImpact, JournalApc = j.JournalApc, JournalSnip = j.JournalSnip,
                JournalSjr = j.JournalSjr, JournalCitescore = j.JournalCitescore, Volume = j.Volume,
                Issue = j.Issue, Year = j.Year, Email = j.Email, Scholar = j.Scholar,
                IndexedArticle = j.IndexedArticle, IndexingImage = j.IndexingImage, IndexingUrl = j.IndexingUrl,
                ImpactFactor = j.ImpactFactor, CrossrefUrl = j.CrossrefUrl, CrossrefImage = j.CrossrefImage,
                IssnNumber = j.IssnNumber, YoutubeLink = j.YoutubeLink, JournalSeoName = j.JournalSeoName,
                CallForPapers = j.CallForPapers, AimsAndScope = j.AimsAndScope, ChiefEditor = j.ChiefEditor,
                JournalImage = j.JournalImage, IsExternalJournal = j.IsExternalJournal,
                JournalStatus = j.JournalStatus, JournalCreatedDate = j.JournalCreatedDate,
                JournalUpdatedDate = j.JournalUpdatedDate
            })
            .FirstOrDefaultAsync();

        if (journal is null) return null;

        journal.Editors = await _context.JournalEditorialBoards
            .Where(b => b.JournalId == id && b.EditorRole != "Editor Profiles")
            .Join(_context.Editors, b => b.EditorId, e => e.EditorId,
                (b, e) => new JournalEditorDto
                {
                    EditorialBoardId = b.EditorialBoardId, EditorId = e.EditorId,
                    EditorName = e.EditorName, EditorDesignation = e.EditorDesignation,
                    ProfileImage = e.ProfileImage, EditorRole = b.EditorRole
                })
            .ToListAsync();

        journal.EditorProfiles = await _context.JournalEditorialBoards
            .Where(b => b.JournalId == id && b.EditorRole == "Editor Profiles")
            .Join(_context.EditorProfiles, b => b.EditorId, ep => ep.EditorId,
                (b, ep) => new EditorProfileBoardDto
                {
                    EditorialBoardId = b.EditorialBoardId, EditorId = ep.EditorId,
                    EditorName = ep.EditorName, EditorImage = ep.EditorImage,
                    EditorDesignation = ep.EditorDesignation
                })
            .ToListAsync();

        journal.Sections = await _context.JournalSections
            .Where(s => s.JournalId == id)
            .ToListAsync();

        return journal;
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null)
    {
        var query = Datatable.Where(j => j.JournalName == name);
        if (excludeId.HasValue)
            query = query.Where(j => j.JournalId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<int> CreateAsync(JournalDto j, string? journalImage, string? indexingImage, string? crossrefImage)
    {
        j.JournalCreatedDate = DateTime.UtcNow;
        j.JournalStatus = 1;
        if (journalImage != null) j.JournalImage = journalImage;
        if (indexingImage != null) j.IndexingImage = indexingImage;
        if (crossrefImage != null) j.CrossrefImage = crossrefImage;

        await _dbSet.AddAsync(j);
        await _context.SaveChangesAsync();
        return j.JournalId;
    }

    public async Task<bool> UpdateAsync(int id, JournalDto j, string? journalImage, string? indexingImage, string? crossrefImage)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return false;

        existing.JournalName = j.JournalName;
        existing.JournalDoi = j.JournalDoi;
        existing.JournalAcceptanceRate = j.JournalAcceptanceRate;
        existing.JournalDecision = j.JournalDecision;
        existing.JournalAcceptancePublic = j.JournalAcceptancePublic;
        existing.JournalCitescore1 = j.JournalCitescore1;
        existing.JournalImpact = j.JournalImpact;
        existing.JournalApc = j.JournalApc;
        existing.JournalSnip = j.JournalSnip;
        existing.JournalSjr = j.JournalSjr;
        existing.JournalCitescore = j.JournalCitescore;
        existing.Volume = j.Volume;
        existing.Issue = j.Issue;
        existing.Year = j.Year;
        existing.Email = j.Email;
        existing.Scholar = j.Scholar;
        existing.IndexedArticle = j.IndexedArticle;
        existing.IndexingUrl = j.IndexingUrl;
        existing.ImpactFactor = j.ImpactFactor;
        existing.CrossrefUrl = j.CrossrefUrl;
        existing.IssnNumber = j.IssnNumber;
        existing.YoutubeLink = j.YoutubeLink;
        existing.JournalSeoName = j.JournalSeoName;
        existing.CallForPapers = j.CallForPapers;
        existing.AimsAndScope = j.AimsAndScope;
        existing.ChiefEditor = j.ChiefEditor;
        existing.IsExternalJournal = j.IsExternalJournal;
        existing.JournalUpdatedDate = DateTime.UtcNow;
        if (journalImage != null) existing.JournalImage = journalImage;
        if (indexingImage != null) existing.IndexingImage = indexingImage;
        if (crossrefImage != null) existing.CrossrefImage = crossrefImage;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(int id, int status)
    {
        int rows = await _context.Journals
            .Where(j => j.JournalId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(j => j.JournalStatus, status)
                .SetProperty(j => j.JournalUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.JournalEditorialBoards.Where(b => b.JournalId == id).ExecuteDeleteAsync();
            await _context.JournalSections.Where(s => s.JournalId == id).ExecuteDeleteAsync();
            int rows = await _context.Journals.Where(j => j.JournalId == id).ExecuteDeleteAsync();
            await transaction.CommitAsync();
            return rows > 0;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ReplaceEditorialBoardAsync(int journalId, Dictionary<string, List<long>> roleEditors)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.JournalEditorialBoards.Where(b => b.JournalId == journalId).ExecuteDeleteAsync();

        var rows = roleEditors
            .SelectMany(kv => kv.Value.Select(editorId =>
                new JournalEditorialBoard { JournalId = journalId, EditorId = editorId, EditorRole = kv.Key }))
            .ToList();

        if (rows.Count > 0)
            await _context.JournalEditorialBoards.AddRangeAsync(rows);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task ReplaceSectionsAsync(int journalId, List<string> sections)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.JournalSections.Where(s => s.JournalId == journalId).ExecuteDeleteAsync();

        if (sections.Count > 0)
            await _context.JournalSections.AddRangeAsync(
                sections.Select(s => new JournalSectionDto { JournalId = journalId, SectionName = s }));

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}
