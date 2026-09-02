using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Editor;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class EditorRepository(ApplicationDbContext context) : Repository<EditorDto>(context), IEditorRepository
{
    public async Task<IEnumerable<EditorDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.EditorName.Contains(search) ||
                                     (e.EditorEmail != null && e.EditorEmail.Contains(search)) ||
                                     (e.EditorPhone != null && e.EditorPhone.Contains(search)));
        return await query
            .OrderBy(e => e.EditorName)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.EditorName.Contains(search) ||
                                     (e.EditorEmail != null && e.EditorEmail.Contains(search)));
        return await query.CountAsync();
    }

    public async Task<EditorDto?> GetByIdAsync(long id)
        => await Datatable.Where(e => e.EditorId == id).FirstOrDefaultAsync();

    public async Task<bool> ExistsByEmailAsync(string email, long? excludeId = null)
    {
        var query = Datatable.Where(e => e.EditorEmail == email);
        if (excludeId.HasValue) query = query.Where(e => e.EditorId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(EditorDto e, string? imageFileName)
    {
        e.EditorStatus = 1;
        e.EditorCreatedDate = DateTime.UtcNow;
        e.EditorPassword ??= "";
        if (imageFileName != null) e.ProfileImage = imageFileName;
        await _dbSet.AddAsync(e);
        await _context.SaveChangesAsync();
        return e.EditorId;
    }

    public async Task<bool> UpdateAsync(long id, EditorDto e, string? imageFileName)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return false;

        existing.EditorName = e.EditorName;
        existing.EditorDesignation = e.EditorDesignation;
        existing.Qualification = e.Qualification;
        existing.EditorEmail = e.EditorEmail;
        existing.EditorPhone = e.EditorPhone;
        existing.EditorAddress = e.EditorAddress;
        existing.EditorBiography = e.EditorBiography;
        existing.EditorResearch = e.EditorResearch;
        existing.EditorOrcid = e.EditorOrcid;
        existing.EditorGoogleScholar = e.EditorGoogleScholar;
        existing.EditorWebsite = e.EditorWebsite;
        existing.EditorUpdatedDate = DateTime.UtcNow;
        if (imageFileName != null) existing.ProfileImage = imageFileName;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.Editors
            .Where(e => e.EditorId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.EditorStatus, status)
                .SetProperty(e => e.EditorUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Editors.Where(e => e.EditorId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<bool> ClearImageAsync(long id)
    {
        int rows = await _context.Editors
            .Where(e => e.EditorId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.ProfileImage, (string?)null)
                .SetProperty(e => e.EditorUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(e => e.EditorId == id).Select(e => e.ProfileImage).FirstOrDefaultAsync();
}
