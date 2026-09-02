using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.EditorProfile;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class EditorProfileRepository(ApplicationDbContext context) : Repository<EditorProfileDto>(context), IEditorProfileRepository
{
    public async Task<IEnumerable<EditorProfileDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.EditorName.Contains(search) ||
                                     (e.EditorEmail != null && e.EditorEmail.Contains(search)));
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

    public async Task<EditorProfileDto?> GetByIdAsync(long id)
        => await Datatable.Where(e => e.EditorId == id).FirstOrDefaultAsync();

    public async Task<bool> ExistsByEmailAsync(string email, long? excludeId = null)
    {
        var query = Datatable.Where(e => e.EditorEmail == email);
        if (excludeId.HasValue) query = query.Where(e => e.EditorId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(EditorProfileDto e, string? imageFileName)
    {
        e.EditorStatus = 1;
        e.EditorCreatedDate = DateTime.UtcNow;
        e.EditorPassword ??= "";
        if (imageFileName != null) e.EditorImage = imageFileName;
        await _dbSet.AddAsync(e);
        await _context.SaveChangesAsync();
        return e.EditorId;
    }

    public async Task<bool> UpdateAsync(long id, EditorProfileDto e, string? imageFileName)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return false;

        existing.EditorName = e.EditorName;
        existing.EditorEmail = e.EditorEmail;
        existing.EditorPhone = e.EditorPhone;
        existing.EditorDesignation = e.EditorDesignation;
        existing.EditorDescription = e.EditorDescription;
        existing.EditorUpdatedDate = DateTime.UtcNow;
        if (imageFileName != null) existing.EditorImage = imageFileName;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.EditorProfiles
            .Where(e => e.EditorId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.EditorStatus, status)
                .SetProperty(e => e.EditorUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.EditorProfiles.Where(e => e.EditorId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<bool> ClearImageAsync(long id)
    {
        int rows = await _context.EditorProfiles
            .Where(e => e.EditorId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.EditorImage, (string?)null)
                .SetProperty(e => e.EditorUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(e => e.EditorId == id).Select(e => e.EditorImage).FirstOrDefaultAsync();
}
