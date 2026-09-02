using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Reviewer;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class ReviewerRepository(ApplicationDbContext context) : Repository<ReviewerDto>(context), IReviewerRepository
{
    public async Task<IEnumerable<ReviewerDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(r => r.ReviewerName.Contains(search) ||
                                     (r.ReviewerEmail != null && r.ReviewerEmail.Contains(search)));
        return await query
            .OrderByDescending(r => r.ReviewerId)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(r => r.ReviewerName.Contains(search) ||
                                     (r.ReviewerEmail != null && r.ReviewerEmail.Contains(search)));
        return await query.CountAsync();
    }

    public async Task<ReviewerDto?> GetByIdAsync(long id)
        => await Datatable.Where(r => r.ReviewerId == id).FirstOrDefaultAsync();

    public async Task<bool> ExistsByEmailAsync(string email, long? excludeId = null)
    {
        var query = Datatable.Where(r => r.ReviewerEmail == email);
        if (excludeId.HasValue) query = query.Where(r => r.ReviewerId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(ReviewerDto r, string? imageFileName)
    {
        r.ReviewerStatus = 1;
        r.ReviewerCreatedDate = DateTime.UtcNow;
        r.ReviewerPassword ??= "";
        if (imageFileName != null) r.ReviewerImage = imageFileName;
        await _dbSet.AddAsync(r);
        await _context.SaveChangesAsync();
        return r.ReviewerId;
    }

    public async Task<bool> UpdateAsync(long id, ReviewerDto r, string? imageFileName)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return false;

        existing.ReviewerName = r.ReviewerName;
        existing.ReviewerEmail = r.ReviewerEmail;
        existing.ReviewerPhone = r.ReviewerPhone;
        existing.ReviewerCountry = r.ReviewerCountry;
        existing.ReviewerDesignation = r.ReviewerDesignation;
        existing.ReviewerAddress = r.ReviewerAddress;
        existing.ReviewerDescription = r.ReviewerDescription;
        existing.ReviewerUpdatedDate = DateTime.UtcNow;
        if (imageFileName != null) existing.ReviewerImage = imageFileName;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.Reviewers
            .Where(r => r.ReviewerId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(r => r.ReviewerStatus, status)
                .SetProperty(r => r.ReviewerUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Reviewers.Where(r => r.ReviewerId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<bool> ClearImageAsync(long id)
    {
        int rows = await _context.Reviewers
            .Where(r => r.ReviewerId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(r => r.ReviewerImage, (string?)null)
                .SetProperty(r => r.ReviewerUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(r => r.ReviewerId == id).Select(r => r.ReviewerImage).FirstOrDefaultAsync();
}
