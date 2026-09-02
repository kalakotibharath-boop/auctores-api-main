using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Author;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class AuthorRepository(ApplicationDbContext context) : Repository<AuthorDto>(context), IAuthorRepository
{
    public async Task<IEnumerable<AuthorDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(a => a.AuthorName.Contains(search) ||
                                     (a.AuthorEmail != null && a.AuthorEmail.Contains(search)) ||
                                     (a.AuthorPhone != null && a.AuthorPhone.Contains(search)));
        return await query
            .OrderByDescending(a => a.AuthorId)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(a => a.AuthorName.Contains(search) ||
                                     (a.AuthorEmail != null && a.AuthorEmail.Contains(search)));
        return await query.CountAsync();
    }

    public async Task<AuthorDto?> GetByIdAsync(long id)
        => await Datatable.Where(a => a.AuthorId == id).FirstOrDefaultAsync();

    public async Task<bool> ExistsByEmailAsync(string email, long? excludeId = null)
    {
        var query = Datatable.Where(a => a.AuthorEmail == email);
        if (excludeId.HasValue) query = query.Where(a => a.AuthorId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(AuthorDto a, string? imageFileName)
    {
        a.AuthorStatus = 1;
        a.AuthorCreatedDate = DateTime.UtcNow;
        a.AuthorPassword = null;
        if (imageFileName != null) a.AuthorImage = imageFileName;
        await _dbSet.AddAsync(a);
        await _context.SaveChangesAsync();
        return a.AuthorId;
    }

    public async Task<bool> UpdateAsync(long id, AuthorDto a, string? imageFileName)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return false;

        existing.AuthorName = a.AuthorName;
        existing.AuthorEmail = a.AuthorEmail;
        existing.AuthorPhone = a.AuthorPhone;
        existing.AuthorCountry = a.AuthorCountry;
        existing.AuthorDesignation = a.AuthorDesignation;
        existing.AuthorDescription = a.AuthorDescription;
        existing.AuthorAddress = a.AuthorAddress;
        existing.AuthorUpdatedDate = DateTime.UtcNow;
        if (imageFileName != null) existing.AuthorImage = imageFileName;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.Authors
            .Where(a => a.AuthorId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.AuthorStatus, status)
                .SetProperty(a => a.AuthorUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Authors.Where(a => a.AuthorId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<bool> ClearImageAsync(long id)
    {
        int rows = await _context.Authors
            .Where(a => a.AuthorId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.AuthorImage, (string?)null)
                .SetProperty(a => a.AuthorUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(a => a.AuthorId == id).Select(a => a.AuthorImage).FirstOrDefaultAsync();
}
