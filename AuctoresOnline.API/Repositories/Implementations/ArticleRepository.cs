using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Article;
using AuctoresOnline.API.Entities;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class ArticleRepository(ApplicationDbContext context) : Repository<ArticleDto>(context), IArticleRepository
{
    public async Task<IEnumerable<ArticleDto>> GetAllAsync(int page, int pageSize, string? search, byte? articleType)
    {
        var query = Datatable
            .Join(_context.Journals, a => a.JournalId, j => j.JournalId,
                (a, j) => new { a, JournalName = j.JournalName, JournalSeoName = j.JournalSeoName });

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.a.ArticleName.Contains(search) || x.a.ArticleSeoName.Contains(search));
        if (articleType.HasValue)
            query = query.Where(x => x.a.ArticleType == articleType.Value);

        return await query
            .OrderByDescending(x => x.a.ArticleCreatedDate)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(x => new ArticleDto
            {
                ArticleId = x.a.ArticleId, ArticleViews = x.a.ArticleViews, ArticleDownloads = x.a.ArticleDownloads,
                ArticleFor = x.a.ArticleFor, JournalId = x.a.JournalId, JournalName = x.JournalName,
                JournalSeoName = x.JournalSeoName, VolumeNo = x.a.VolumeNo, IssueNo = x.a.IssueNo,
                ArticleType = x.a.ArticleType, ArticleName = x.a.ArticleName, ArticleSeoName = x.a.ArticleSeoName,
                ArticleDoi = x.a.ArticleDoi, CorrespondingAuthor = x.a.CorrespondingAuthor,
                ReceivedDate = x.a.ReceivedDate, AcceptedDate = x.a.AcceptedDate, PublishedDate = x.a.PublishedDate,
                Citation = x.a.Citation, Copyright = x.a.Copyright, ArticleInformation = x.a.ArticleInformation,
                ArticleAbstract = x.a.ArticleAbstract, DisplayArticleInformation = x.a.DisplayArticleInformation,
                DisplayArticleAbstract = x.a.DisplayArticleAbstract, AddressDesc = x.a.AddressDesc,
                AbstractKeywords = x.a.AbstractKeywords, ArticlePdf = x.a.ArticlePdf,
                ArticleStatus = x.a.ArticleStatus, ArticleCreatedDate = x.a.ArticleCreatedDate,
                ArticleUpdatedDate = x.a.ArticleUpdatedDate
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search, byte? articleType)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(a => a.ArticleName.Contains(search));
        if (articleType.HasValue)
            query = query.Where(a => a.ArticleType == articleType.Value);
        return await query.CountAsync();
    }

    public async Task<ArticleDto?> GetByIdAsync(long id)
    {
        return await Datatable
            .Where(a => a.ArticleId == id)
            .Join(_context.Journals, a => a.JournalId, j => j.JournalId,
                (a, j) => new ArticleDto
                {
                    ArticleId = a.ArticleId, ArticleViews = a.ArticleViews, ArticleDownloads = a.ArticleDownloads,
                    ArticleFor = a.ArticleFor, JournalId = a.JournalId, JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName, VolumeNo = a.VolumeNo, IssueNo = a.IssueNo,
                    ArticleType = a.ArticleType, ArticleName = a.ArticleName, ArticleSeoName = a.ArticleSeoName,
                    ArticleDoi = a.ArticleDoi, CorrespondingAuthor = a.CorrespondingAuthor,
                    ReceivedDate = a.ReceivedDate, AcceptedDate = a.AcceptedDate, PublishedDate = a.PublishedDate,
                    Citation = a.Citation, Copyright = a.Copyright, ArticleInformation = a.ArticleInformation,
                    ArticleAbstract = a.ArticleAbstract, DisplayArticleInformation = a.DisplayArticleInformation,
                    DisplayArticleAbstract = a.DisplayArticleAbstract, AddressDesc = a.AddressDesc,
                    AbstractKeywords = a.AbstractKeywords, ArticlePdf = a.ArticlePdf,
                    ArticleStatus = a.ArticleStatus, ArticleCreatedDate = a.ArticleCreatedDate,
                    ArticleUpdatedDate = a.ArticleUpdatedDate
                })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByNameAndJournalAsync(string name, int journalId, long? excludeId = null)
    {
        var query = Datatable.Where(a => a.ArticleName == name && a.JournalId == journalId);
        if (excludeId.HasValue) query = query.Where(a => a.ArticleId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(ArticleDto a, string pdfFileName)
    {
        a.ArticlePdf = pdfFileName;
        a.ArticleStatus = 1;
        a.ArticleCreatedDate = DateTime.UtcNow;
        await _dbSet.AddAsync(a);
        await _context.SaveChangesAsync();
        return a.ArticleId;
    }

    public async Task<bool> UpdateAsync(long id, ArticleDto a, string? newPdfFileName)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return false;

        existing.ArticleFor = a.ArticleFor;
        existing.JournalId = a.JournalId;
        existing.VolumeNo = a.VolumeNo;
        existing.IssueNo = a.IssueNo;
        existing.ArticleType = a.ArticleType;
        existing.ArticleName = a.ArticleName;
        existing.ArticleSeoName = a.ArticleSeoName;
        existing.ArticleDoi = a.ArticleDoi;
        existing.CorrespondingAuthor = a.CorrespondingAuthor;
        existing.ReceivedDate = a.ReceivedDate;
        existing.AcceptedDate = a.AcceptedDate;
        existing.PublishedDate = a.PublishedDate;
        existing.Citation = a.Citation;
        existing.Copyright = a.Copyright;
        existing.ArticleInformation = a.ArticleInformation;
        existing.ArticleAbstract = a.ArticleAbstract;
        existing.DisplayArticleInformation = a.DisplayArticleInformation;
        existing.DisplayArticleAbstract = a.DisplayArticleAbstract;
        existing.AddressDesc = a.AddressDesc;
        existing.AbstractKeywords = a.AbstractKeywords;
        existing.ArticleUpdatedDate = DateTime.UtcNow;
        if (newPdfFileName != null) existing.ArticlePdf = newPdfFileName;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.Articles
            .Where(a => a.ArticleId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.ArticleStatus, status)
                .SetProperty(a => a.ArticleUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> UpdateTypeAsync(long id, byte type)
    {
        int rows = await _context.Articles
            .Where(a => a.ArticleId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.ArticleType, type)
                .SetProperty(a => a.ArticleUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.ArticleAuthors.Where(a => a.ArticleId == id).ExecuteDeleteAsync();
            await _context.ArticleReferences.Where(r => r.ReferenceArticleId == id).ExecuteDeleteAsync();
            await _context.ArticleDetails.Where(d => d.DetailsArticleId == id).ExecuteDeleteAsync();
            int rows = await _context.Articles.Where(a => a.ArticleId == id).ExecuteDeleteAsync();
            await transaction.CommitAsync();
            return rows > 0;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ReplaceAuthorsAsync(long articleId, List<ArticleAuthorRequest> authors)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.ArticleAuthors.Where(a => a.ArticleId == articleId).ExecuteDeleteAsync();
        if (authors.Count > 0)
            await _context.ArticleAuthors.AddRangeAsync(authors.Select(a => new ArticleAuthorDto
            {
                ArticleId = articleId, AuthorName = a.AuthorName, AuthorNo = a.AuthorNo,
                AuthorSubhead = a.AuthorSubhead, AuthorEmail = a.AuthorEmail,
                AuthorOrcid = a.AuthorOrcid, AuthorAddress = a.AuthorAddress
            }));
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task ReplaceReferencesAsync(long articleId, List<ArticleReferenceRequest> refs)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.ArticleReferences.Where(r => r.ReferenceArticleId == articleId).ExecuteDeleteAsync();
        if (refs.Count > 0)
            await _context.ArticleReferences.AddRangeAsync(refs.Select(r => new ArticleReferenceDto
            {
                ReferenceArticleId = articleId, ReferenceData = r.ReferenceData,
                ReferenceGoogleLink = r.ReferenceGoogleLink, ReferencePublisherLink = r.ReferencePublisherLink
            }));
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task<IEnumerable<ArticleDetailDto>> GetDetailsAsync(long articleId)
        => await _context.ArticleDetails
            .Where(d => d.DetailsArticleId == articleId)
            .OrderBy(d => d.DetailsId)
            .ToListAsync();

    public async Task<ArticleDetailDto?> GetDetailByIdAsync(long detailsId)
        => await _context.ArticleDetails.FindAsync(detailsId);

    public async Task<long> SaveDetailAsync(long articleId, SaveArticleDetailRequest req)
    {
        if (req.DetailsId.HasValue && req.DetailsId > 0)
        {
            await _context.ArticleDetails
                .Where(d => d.DetailsId == req.DetailsId.Value)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(d => d.DetailsHeading, req.DetailsHeading)
                    .SetProperty(d => d.DetailsDescription, req.DetailsDescription)
                    .SetProperty(d => d.DisplayDetailsDescription, req.DisplayDetailsDescription));
            return req.DetailsId.Value;
        }

        var detail = new ArticleDetailDto
        {
            DetailsArticleId = articleId, DetailsHeading = req.DetailsHeading,
            DetailsDescription = req.DetailsDescription,
            DisplayDetailsDescription = req.DisplayDetailsDescription,
            CreatedDate = DateTime.UtcNow
        };
        await _context.ArticleDetails.AddAsync(detail);
        await _context.SaveChangesAsync();
        return detail.DetailsId;
    }

    public async Task<bool> DeleteDetailAsync(long detailsId)
    {
        int rows = await _context.ArticleDetails.Where(d => d.DetailsId == detailsId).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task UpdateCkEditorImagesAsync(long articleId, string dataOf)
    {
        await _context.TempCkeditorImages
            .Where(t => t.ArticleId == articleId && t.DataOf == dataOf)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.FileSaved, "no"));
    }
}
