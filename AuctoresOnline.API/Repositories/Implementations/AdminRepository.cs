using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Admin;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class AdminRepository(ApplicationDbContext context) : Repository<AdminDto>(context), IAdminRepository
{
    public async Task<AdminDto?> GetByUsernameAsync(string username)
        => await Datatable.Where(a => a.Username == username).FirstOrDefaultAsync();

    public async Task<AdminDto?> GetByIdAsync(int id)
        => await Datatable.Where(a => a.Id == id).FirstOrDefaultAsync();

    public async Task<string?> GetPasswordAsync(int id)
        => await Datatable.Where(a => a.Id == id).Select(a => a.Password).FirstOrDefaultAsync();

    public async Task<string?> GetUsernamePasswordAsync(string username)
        => await Datatable.Where(a => a.Username == username).Select(a => a.Password).FirstOrDefaultAsync();

    public async Task<bool> UpdateProfileAsync(int id, string name, string email, string mobile)
    {
        int rows = await _context.Admins
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Name, name)
                .SetProperty(a => a.Email, email)
                .SetProperty(a => a.Mobile, mobile)
                .SetProperty(a => a.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> UpdatePasswordAsync(int id, string passwordHash)
    {
        int rows = await _context.Admins
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Password, passwordHash)
                .SetProperty(a => a.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<DashboardCountsDto> GetDashboardCountsAsync()
    {
        return new DashboardCountsDto(
            TotalContacts: await _context.Contacts.CountAsync(),
            TotalTestimonials: await _context.Testimonials.CountAsync(),
            TotalSubscribers: await _context.SubscriptionList.CountAsync(s => s.SubscriptionStatus == 1),
            TotalBanners: await _context.Banners.CountAsync(),
            TotalCollaborators: await _context.Collaborators.CountAsync(),
            TotalJournals: await _context.Journals.CountAsync(j => j.JournalStatus == 1),
            TotalManuscripts: await _context.ManuscriptRequests.CountAsync(),
            TotalAuthors: await _context.Authors.CountAsync(),
            TotalEditors: await _context.Editors.CountAsync(),
            TotalEditorProfiles: await _context.EditorProfiles.CountAsync(),
            TotalMemberedIn: await _context.MemberedIn.CountAsync(),
            TotalReviewers: await _context.Reviewers.CountAsync(),
            TotalArticlesInPress: await _context.Articles.CountAsync(a => a.ArticleType == 0),
            TotalCurrentIssues: await _context.Articles.CountAsync(a => a.ArticleType == 1),
            TotalArchives: await _context.Articles.CountAsync(a => a.ArticleType == 2)
        );
    }
}
