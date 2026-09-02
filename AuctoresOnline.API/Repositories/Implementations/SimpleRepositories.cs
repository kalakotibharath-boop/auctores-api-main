using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Models.Document;
using AuctoresOnline.API.Models.Manuscript;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Repositories.Interfaces;

namespace AuctoresOnline.API.Repositories.Implementations;

public class BannerRepository(ApplicationDbContext context) : Repository<BannerDto>(context), IBannerRepository
{
    public async Task<IEnumerable<BannerDto>> GetAllAsync()
        => await Datatable.OrderByDescending(b => b.BannerId).ToListAsync();

    public async Task<BannerDto?> GetByIdAsync(long id)
        => await Datatable.Where(b => b.BannerId == id).FirstOrDefaultAsync();

    public async Task<long> CreateAsync(BannerDto b, string? imageFileName)
    {
        b.BannerStatus = 1;
        b.CreatedDate = DateTime.UtcNow;
        if (imageFileName != null) b.BannerImage = imageFileName;
        await _dbSet.AddAsync(b);
        await _context.SaveChangesAsync();
        return b.BannerId;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.Banners
            .Where(b => b.BannerId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.BannerStatus, status)
                .SetProperty(b => b.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Banners.Where(b => b.BannerId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(b => b.BannerId == id).Select(b => b.BannerImage).FirstOrDefaultAsync();
}

public class CollaboratorRepository(ApplicationDbContext context) : Repository<CollaboratorDto>(context), ICollaboratorRepository
{
    public async Task<IEnumerable<CollaboratorDto>> GetAllAsync()
        => await Datatable.OrderByDescending(c => c.CollaboratorId).ToListAsync();

    public async Task<CollaboratorDto?> GetByIdAsync(long id)
        => await Datatable.Where(c => c.CollaboratorId == id).FirstOrDefaultAsync();

    public async Task<long> CreateAsync(CollaboratorDto c, string? imageFileName)
    {
        c.CollaboratorStatus = 1;
        c.CreatedDate = DateTime.UtcNow;
        if (imageFileName != null) c.CollaboratorImage = imageFileName;
        await _dbSet.AddAsync(c);
        await _context.SaveChangesAsync();
        return c.CollaboratorId;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.Collaborators
            .Where(c => c.CollaboratorId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.CollaboratorStatus, status)
                .SetProperty(c => c.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Collaborators.Where(c => c.CollaboratorId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(c => c.CollaboratorId == id).Select(c => c.CollaboratorImage).FirstOrDefaultAsync();
}

public class ContactRepository(ApplicationDbContext context) : Repository<ContactDto>(context), IContactRepository
{
    public async Task<IEnumerable<ContactDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => (c.ContactName != null && c.ContactName.Contains(search)) ||
                                     (c.ContactEmail != null && c.ContactEmail.Contains(search)) ||
                                     (c.ContactSubject != null && c.ContactSubject.Contains(search)));
        return await query
            .OrderByDescending(c => c.ContactId)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => (c.ContactName != null && c.ContactName.Contains(search)) ||
                                     (c.ContactEmail != null && c.ContactEmail.Contains(search)));
        return await query.CountAsync();
    }

    public async Task<ContactDto?> GetByIdAsync(long id)
        => await Datatable.Where(c => c.ContactId == id).FirstOrDefaultAsync();

    public async Task<bool> UpdateStatusAsync(long id, string status)
    {
        int rows = await _context.Contacts
            .Where(c => c.ContactId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.ContactStatus, status)
                .SetProperty(c => c.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Contacts.Where(c => c.ContactId == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}

public class TestimonialRepository(ApplicationDbContext context) : Repository<TestimonialDto>(context), ITestimonialRepository
{
    public async Task<IEnumerable<TestimonialDto>> GetAllAsync()
        => await Datatable.OrderByDescending(t => t.TestimonialCreatedDate).ToListAsync();

    public async Task<TestimonialDto?> GetByIdAsync(long id)
        => await Datatable.Where(t => t.TestimonialId == id).FirstOrDefaultAsync();

    public async Task<long> CreateAsync(TestimonialDto t, string? imageFileName)
    {
        t.TestimonialStatus = 1;
        t.TestimonialCreatedDate = DateTime.UtcNow;
        if (imageFileName != null) t.TestimonialImage = imageFileName;
        await _dbSet.AddAsync(t);
        await _context.SaveChangesAsync();
        return t.TestimonialId;
    }

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.Testimonials
            .Where(t => t.TestimonialId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.TestimonialStatus, status)
                .SetProperty(t => t.TestimonialUpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Testimonials.Where(t => t.TestimonialId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(t => t.TestimonialId == id).Select(t => t.TestimonialImage).FirstOrDefaultAsync();
}

public class SubscriberRepository(ApplicationDbContext context) : Repository<SubscriberDto>(context), ISubscriberRepository
{
    public async Task<IEnumerable<SubscriberDto>> GetAllAsync()
        => await Datatable
            .GroupJoin(_context.Journals, s => s.SubscribedForId, j => (long?)j.JournalId,
                (s, journals) => new { s, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new SubscriberDto
                {
                    SubscriptionId = x.s.SubscriptionId, EmailId = x.s.EmailId,
                    SubscribedFor = x.s.SubscribedFor, SubscribedForId = x.s.SubscribedForId,
                    JournalName = j != null ? j.JournalName : null,
                    SubscriptionStatus = x.s.SubscriptionStatus,
                    CreatedDate = x.s.CreatedDate, UpdatedDate = x.s.UpdatedDate
                })
            .OrderByDescending(s => s.CreatedDate)
            .ToListAsync();

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.SubscriptionList
            .Where(s => s.SubscriptionId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.SubscriptionStatus, status)
                .SetProperty(x => x.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.SubscriptionList.Where(s => s.SubscriptionId == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}

public class DocumentRepository(ApplicationDbContext context) : Repository<DocumentDto>(context), IDocumentRepository
{
    public async Task<IEnumerable<DocumentDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable
            .GroupJoin(_context.Journals, d => d.JournalId, j => (long?)j.JournalId,
                (d, journals) => new { d, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new DocumentDto
                {
                    DocId = x.d.DocId, Title = x.d.Title, PageSlug = x.d.PageSlug, DocData = x.d.DocData,
                    JournalId = x.d.JournalId, JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    SeoKeywords = x.d.SeoKeywords, Type = x.d.Type, Status = x.d.Status,
                    CreatedBy = x.d.CreatedBy, CreatedDate = x.d.CreatedDate, UpdatedDate = x.d.UpdatedDate
                });

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(d => (d.Title != null && d.Title.Contains(search)) ||
                                     (d.PageSlug != null && d.PageSlug.Contains(search)));

        return await query.OrderByDescending(d => d.CreatedDate)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(d => d.Title != null && d.Title.Contains(search));
        return await query.CountAsync();
    }

    public async Task<DocumentDto?> GetByIdAsync(long id)
        => await Datatable
            .Where(d => d.DocId == id)
            .GroupJoin(_context.Journals, d => d.JournalId, j => (long?)j.JournalId,
                (d, journals) => new { d, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new DocumentDto
                {
                    DocId = x.d.DocId, Title = x.d.Title, PageSlug = x.d.PageSlug, DocData = x.d.DocData,
                    JournalId = x.d.JournalId, JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    SeoKeywords = x.d.SeoKeywords, Type = x.d.Type, Status = x.d.Status,
                    CreatedBy = x.d.CreatedBy, CreatedDate = x.d.CreatedDate, UpdatedDate = x.d.UpdatedDate
                })
            .FirstOrDefaultAsync();

    public async Task<bool> ExistsBySlugAsync(string slug, long? excludeId = null)
    {
        var query = Datatable.Where(d => d.PageSlug == slug);
        if (excludeId.HasValue) query = query.Where(d => d.DocId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(CreateDocumentRequest req)
    {
        var doc = new DocumentDto
        {
            Title = req.Title, PageSlug = req.PageSlug, DocData = req.DocData,
            JournalId = req.JournalId, SeoKeywords = req.SeoKeywords, Type = req.Type,
            Status = 1, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow
        };
        await _dbSet.AddAsync(doc);
        await _context.SaveChangesAsync();
        return doc.DocId;
    }

    public async Task<bool> UpdateAsync(long id, CreateDocumentRequest req)
    {
        int rows = await _context.Documents
            .Where(d => d.DocId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.Title, req.Title)
                .SetProperty(d => d.PageSlug, req.PageSlug)
                .SetProperty(d => d.DocData, req.DocData)
                .SetProperty(d => d.JournalId, req.JournalId)
                .SetProperty(d => d.SeoKeywords, req.SeoKeywords)
                .SetProperty(d => d.Type, req.Type)
                .SetProperty(d => d.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> UpdateStatusAsync(long id, int status)
    {
        int rows = await _context.Documents
            .Where(d => d.DocId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.Status, status)
                .SetProperty(d => d.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.Documents.Where(d => d.DocId == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}

public class ManuscriptRequestRepository(ApplicationDbContext context) : Repository<ManuscriptRequestDto>(context), IManuscriptRequestRepository
{
    public async Task<IEnumerable<ManuscriptRequestDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable
            .GroupJoin(_context.Journals, mr => mr.JournalId, j => (int?)j.JournalId,
                (mr, journals) => new { mr, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new ManuscriptRequestDto
                {
                    RequestId = x.mr.RequestId, FirstName = x.mr.FirstName, LastName = x.mr.LastName,
                    Name = (x.mr.FirstName ?? "") + " " + (x.mr.LastName ?? ""),
                    Email = x.mr.Email, Phone = x.mr.Phone, Country = x.mr.Country, Orcid = x.mr.Orcid,
                    Address = x.mr.Address, JournalId = x.mr.JournalId,
                    JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    ManuscriptTitle = x.mr.ManuscriptTitle, ArticleType = x.mr.ArticleType,
                    Abstract = x.mr.Abstract, Keywords = x.mr.Keywords, BioData = x.mr.BioData,
                    Files = x.mr.Files, CoverLetter = x.mr.CoverLetter, Status = x.mr.Status,
                    CreatedDate = x.mr.CreatedDate, UpdatedDate = x.mr.UpdatedDate
                });

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(mr => (mr.Email != null && mr.Email.Contains(search)) ||
                                      (mr.Country != null && mr.Country.Contains(search)) ||
                                      (mr.JournalName != null && mr.JournalName.Contains(search)));

        return await query.OrderByDescending(mr => mr.CreatedDate)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(mr => (mr.Email != null && mr.Email.Contains(search)) ||
                                      (mr.Country != null && mr.Country.Contains(search)));
        return await query.CountAsync();
    }

    public async Task<ManuscriptRequestDto?> GetByIdAsync(long id)
        => await Datatable
            .Where(mr => mr.RequestId == id)
            .GroupJoin(_context.Journals, mr => mr.JournalId, j => (int?)j.JournalId,
                (mr, journals) => new { mr, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new ManuscriptRequestDto
                {
                    RequestId = x.mr.RequestId, FirstName = x.mr.FirstName, LastName = x.mr.LastName,
                    Name = (x.mr.FirstName ?? "") + " " + (x.mr.LastName ?? ""),
                    Email = x.mr.Email, Phone = x.mr.Phone, Country = x.mr.Country, Orcid = x.mr.Orcid,
                    Address = x.mr.Address, JournalId = x.mr.JournalId,
                    JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    ManuscriptTitle = x.mr.ManuscriptTitle, ArticleType = x.mr.ArticleType,
                    Abstract = x.mr.Abstract, Keywords = x.mr.Keywords, BioData = x.mr.BioData,
                    Files = x.mr.Files, CoverLetter = x.mr.CoverLetter, Status = x.mr.Status,
                    CreatedDate = x.mr.CreatedDate, UpdatedDate = x.mr.UpdatedDate
                })
            .FirstOrDefaultAsync();

    public async Task<bool> UpdateStatusAsync(long id, byte status)
    {
        int rows = await _context.ManuscriptRequests
            .Where(mr => mr.RequestId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(mr => mr.Status, (int)status)
                .SetProperty(mr => mr.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.ManuscriptRequests.Where(mr => mr.RequestId == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<string?> GetFilesAsync(long id)
        => await Datatable.Where(mr => mr.RequestId == id).Select(mr => mr.Files).FirstOrDefaultAsync();
}

public class ApcRepository(ApplicationDbContext context) : Repository<ApcDto>(context), IApcRepository
{
    public async Task<IEnumerable<ApcDto>> GetAllAsync()
        => await Datatable
            .Join(_context.Journals, a => a.JournalId, j => j.JournalId,
                (a, j) => new ApcDto
                {
                    ApcId = a.ApcId, JournalId = a.JournalId, JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName, ApcAmount = a.ApcAmount,
                    CreatedDate = a.CreatedDate, UpdatedDate = a.UpdatedDate
                })
            .OrderBy(a => a.JournalName)
            .ToListAsync();

    public async Task<ApcDto?> GetByIdAsync(long id)
        => await Datatable
            .Where(a => a.ApcId == id)
            .Join(_context.Journals, a => a.JournalId, j => j.JournalId,
                (a, j) => new ApcDto
                {
                    ApcId = a.ApcId, JournalId = a.JournalId, JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName, ApcAmount = a.ApcAmount,
                    CreatedDate = a.CreatedDate, UpdatedDate = a.UpdatedDate
                })
            .FirstOrDefaultAsync();

    public async Task<bool> ExistsByJournalAsync(int journalId, long? excludeId = null)
    {
        var query = Datatable.Where(a => a.JournalId == journalId);
        if (excludeId.HasValue) query = query.Where(a => a.ApcId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(CreateApcRequest req)
    {
        var apc = new ApcDto { JournalId = req.JournalId, ApcAmount = req.ApcAmount, CreatedDate = DateTime.UtcNow };
        await _dbSet.AddAsync(apc);
        await _context.SaveChangesAsync();
        return apc.ApcId;
    }

    public async Task<bool> UpdateAsync(long id, CreateApcRequest req)
    {
        int rows = await _context.ArticleProcessingCharges
            .Where(a => a.ApcId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.JournalId, req.JournalId)
                .SetProperty(a => a.ApcAmount, req.ApcAmount)
                .SetProperty(a => a.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.ArticleProcessingCharges.Where(a => a.ApcId == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}

public class AbstractingIndexingRepository(ApplicationDbContext context) : Repository<AbstractingIndexingDto>(context), IAbstractingIndexingRepository
{
    public async Task<IEnumerable<AbstractingIndexingDto>> GetAllAsync()
        => await Datatable
            .Join(_context.Journals, ai => ai.JournalId, j => j.JournalId,
                (ai, j) => new AbstractingIndexingDto
                {
                    AbstractionId = ai.AbstractionId, JournalId = ai.JournalId, JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName, AbstractionUrl = ai.AbstractionUrl,
                    AbstractionTitle = ai.AbstractionTitle, Status = ai.Status,
                    CreatedDate = ai.CreatedDate, UpdatedDate = ai.UpdatedDate
                })
            .OrderBy(ai => ai.JournalName)
            .ToListAsync();

    public async Task<AbstractingIndexingDto?> GetByIdAsync(long id)
        => await Datatable
            .Where(ai => ai.AbstractionId == id)
            .Join(_context.Journals, ai => ai.JournalId, j => j.JournalId,
                (ai, j) => new AbstractingIndexingDto
                {
                    AbstractionId = ai.AbstractionId, JournalId = ai.JournalId, JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName, AbstractionUrl = ai.AbstractionUrl,
                    AbstractionTitle = ai.AbstractionTitle, Status = ai.Status,
                    CreatedDate = ai.CreatedDate, UpdatedDate = ai.UpdatedDate
                })
            .FirstOrDefaultAsync();

    public async Task<bool> ExistsByJournalAndTitleAsync(int journalId, string title, long? excludeId = null)
    {
        var query = Datatable.Where(ai => ai.JournalId == journalId && ai.AbstractionTitle == title);
        if (excludeId.HasValue) query = query.Where(ai => ai.AbstractionId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(CreateAbstractingIndexingRequest req)
    {
        var entity = new AbstractingIndexingDto
        {
            JournalId = req.JournalId, AbstractionUrl = req.AbstractionUrl,
            AbstractionTitle = req.AbstractionTitle, Status = 1, CreatedDate = DateTime.UtcNow
        };
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.AbstractionId;
    }

    public async Task<bool> UpdateAsync(long id, CreateAbstractingIndexingRequest req)
    {
        int rows = await _context.AbstractingIndexing
            .Where(ai => ai.AbstractionId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ai => ai.JournalId, req.JournalId)
                .SetProperty(ai => ai.AbstractionUrl, req.AbstractionUrl)
                .SetProperty(ai => ai.AbstractionTitle, req.AbstractionTitle)
                .SetProperty(ai => ai.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.AbstractingIndexing.Where(ai => ai.AbstractionId == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}

public class PubmedIndexRepository(ApplicationDbContext context) : Repository<PubmedIndexDto>(context), IPubmedIndexRepository
{
    public async Task<IEnumerable<PubmedIndexDto>> GetAllAsync()
        => await Datatable
            .Join(_context.Journals, p => p.JournalId, j => j.JournalId,
                (p, j) => new PubmedIndexDto
                {
                    PubmedIndexId = p.PubmedIndexId, JournalId = p.JournalId, JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName, IndexText = p.IndexText,
                    PubmedUrl = p.PubmedUrl, PmcUrl = p.PmcUrl,
                    CreatedDate = p.CreatedDate, UpdatedDate = p.UpdatedDate
                })
            .OrderBy(p => p.JournalName)
            .ToListAsync();

    public async Task<PubmedIndexDto?> GetByIdAsync(long id)
        => await Datatable
            .Where(p => p.PubmedIndexId == id)
            .Join(_context.Journals, p => p.JournalId, j => j.JournalId,
                (p, j) => new PubmedIndexDto
                {
                    PubmedIndexId = p.PubmedIndexId, JournalId = p.JournalId, JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName, IndexText = p.IndexText,
                    PubmedUrl = p.PubmedUrl, PmcUrl = p.PmcUrl,
                    CreatedDate = p.CreatedDate, UpdatedDate = p.UpdatedDate
                })
            .FirstOrDefaultAsync();

    public async Task<bool> ExistsByJournalAndTextAsync(int journalId, string indexText, long? excludeId = null)
    {
        var query = Datatable.Where(p => p.JournalId == journalId && p.IndexText == indexText);
        if (excludeId.HasValue) query = query.Where(p => p.PubmedIndexId != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<long> CreateAsync(CreatePubmedIndexRequest req)
    {
        var entity = new PubmedIndexDto
        {
            JournalId = req.JournalId, IndexText = req.IndexText,
            PubmedUrl = req.PubmedUrl, PmcUrl = req.PmcUrl, CreatedDate = DateTime.UtcNow
        };
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.PubmedIndexId;
    }

    public async Task<bool> UpdateAsync(long id, CreatePubmedIndexRequest req)
    {
        int rows = await _context.PubmedIndex
            .Where(p => p.PubmedIndexId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.JournalId, req.JournalId)
                .SetProperty(p => p.IndexText, req.IndexText)
                .SetProperty(p => p.PubmedUrl, req.PubmedUrl)
                .SetProperty(p => p.PmcUrl, req.PmcUrl)
                .SetProperty(p => p.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.PubmedIndex.Where(p => p.PubmedIndexId == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}

public class MemberedInRepository(ApplicationDbContext context) : Repository<MemberedInDto>(context), IMemberedInRepository
{
    public async Task<IEnumerable<MemberedInDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable
            .Join(_context.Journals, m => m.JournalId, j => j.JournalId,
                (m, j) => new MemberedInDto
                {
                    Id = m.Id, JournalId = m.JournalId, JournalName = j.JournalName,
                    Name = m.Name, Image = m.Image, Url = m.Url, Status = m.Status,
                    CreatedDate = m.CreatedDate, UpdatedDate = m.UpdatedDate
                });

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(m => (m.Name != null && m.Name.Contains(search)) ||
                                     (m.JournalName != null && m.JournalName.Contains(search)));

        return await query.OrderBy(m => m.Name)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(m => m.Name != null && m.Name.Contains(search));
        return await query.CountAsync();
    }

    public async Task<MemberedInDto?> GetByIdAsync(long id)
        => await Datatable
            .Where(m => m.Id == id)
            .Join(_context.Journals, m => m.JournalId, j => j.JournalId,
                (m, j) => new MemberedInDto
                {
                    Id = m.Id, JournalId = m.JournalId, JournalName = j.JournalName,
                    Name = m.Name, Image = m.Image, Url = m.Url, Status = m.Status,
                    CreatedDate = m.CreatedDate, UpdatedDate = m.UpdatedDate
                })
            .FirstOrDefaultAsync();

    public async Task<long> CreateAsync(CreateMemberedInRequest req, string? imageFileName)
    {
        var entity = new MemberedInDto
        {
            JournalId = req.JournalId, Name = req.Name, Image = imageFileName, Url = req.Url,
            Status = 1, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow
        };
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(long id, CreateMemberedInRequest req, string? imageFileName)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return false;
        existing.JournalId = req.JournalId;
        existing.Name = req.Name;
        existing.Url = req.Url;
        existing.UpdatedDate = DateTime.UtcNow;
        if (imageFileName != null) existing.Image = imageFileName;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(long id, int status)
    {
        int rows = await _context.MemberedIn
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.Status, status)
                .SetProperty(m => m.UpdatedDate, DateTime.UtcNow));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.MemberedIn.Where(m => m.Id == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task<string?> GetImageFileNameAsync(long id)
        => await Datatable.Where(m => m.Id == id).Select(m => m.Image).FirstOrDefaultAsync();
}

public class BecomeRequestRepository(ApplicationDbContext context) : Repository<BecomeRequestDto>(context), IBecomeRequestRepository
{
    public async Task<IEnumerable<BecomeRequestDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = Datatable
            .GroupJoin(_context.Journals, mr => mr.JournalId, j => (int?)j.JournalId,
                (mr, journals) => new { mr, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new BecomeRequestDto
                {
                    RequestId = x.mr.RequestId, Name = x.mr.Name, Email = x.mr.Email,
                    PhoneNumber = x.mr.PhoneNumber, Designation = x.mr.Designation, Country = x.mr.Country,
                    BioData = x.mr.BioData, Files = x.mr.Files, Type = x.mr.Type, JournalId = x.mr.JournalId,
                    JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    CreatedDate = x.mr.CreatedDate
                });

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(mr => (mr.Name != null && mr.Name.Contains(search)) ||
                                      (mr.Email != null && mr.Email.Contains(search)));

        return await query.OrderByDescending(mr => mr.RequestId)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? search)
    {
        var query = Datatable;
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(mr => (mr.Name != null && mr.Name.Contains(search)) ||
                                      (mr.Email != null && mr.Email.Contains(search)));
        return await query.CountAsync();
    }

    public async Task<BecomeRequestDto?> GetByIdAsync(long id)
        => await Datatable
            .Where(mr => mr.RequestId == id)
            .GroupJoin(_context.Journals, mr => mr.JournalId, j => (int?)j.JournalId,
                (mr, journals) => new { mr, journals })
            .SelectMany(x => x.journals.DefaultIfEmpty(),
                (x, j) => new BecomeRequestDto
                {
                    RequestId = x.mr.RequestId, Name = x.mr.Name, Email = x.mr.Email,
                    PhoneNumber = x.mr.PhoneNumber, Designation = x.mr.Designation, Country = x.mr.Country,
                    BioData = x.mr.BioData, Files = x.mr.Files, Type = x.mr.Type, JournalId = x.mr.JournalId,
                    JournalName = j != null ? j.JournalName : null,
                    JournalSeoName = j != null ? j.JournalSeoName : null,
                    CreatedDate = x.mr.CreatedDate
                })
            .FirstOrDefaultAsync();

    public async Task<bool> DeleteAsync(long id)
    {
        int rows = await _context.BecomeRequests.Where(mr => mr.RequestId == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}
