using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserDocumentService(ApplicationDbContext context) : IUserDocumentService
{
    public async Task<ServiceResult<IEnumerable<DocumentPublicDto>>> GetAllDocumentsAsync()
    {
        var docs = await context.Documents
            .Where(d => d.Status == 1)
            .OrderBy(d => d.CreatedDate)
            .Select(d => new DocumentPublicDto
            {
                DocId = d.DocId,
                Title = d.Title,
                PageSlug = d.PageSlug,
                SeoKeywords = d.SeoKeywords
            })
            .ToListAsync();

        return ServiceResult<IEnumerable<DocumentPublicDto>>.Ok(docs);
    }

    public async Task<ServiceResult<DocumentPublicDetailDto>> GetDocumentBySlugAsync(string slug)
    {
        var page = await context.Documents
            .Where(d => d.Status == 1 && d.PageSlug == slug)
            .FirstOrDefaultAsync();

        if (page is null)
            return ServiceResult<DocumentPublicDetailDto>.NotFound("Document page not found.");

        var allPages = await context.Documents
            .Where(d => d.Status == 1)
            .OrderBy(d => d.CreatedDate)
            .Select(d => new DocumentPublicDto
            {
                DocId = d.DocId,
                Title = d.Title,
                PageSlug = d.PageSlug,
                SeoKeywords = d.SeoKeywords
            })
            .ToListAsync();

        return ServiceResult<DocumentPublicDetailDto>.Ok(new DocumentPublicDetailDto
        {
            DocId = page.DocId,
            Title = page.Title,
            PageSlug = page.PageSlug,
            DocData = page.DocData,
            SeoKeywords = page.SeoKeywords,
            AllPages = allPages
        });
    }
}
