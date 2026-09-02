using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Document;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class DocumentService(IDocumentRepository repo, FileUploadHelper fileHelper) : IDocumentService
{
    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<DocumentDto>> GetByIdAsync(long id)
    {
        var doc = await repo.GetByIdAsync(id);
        if (doc is null) return ServiceResult<DocumentDto>.NotFound("Document not found.");
        return ServiceResult<DocumentDto>.Ok(doc);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateDocumentRequest req)
    {
        if (!string.IsNullOrEmpty(req.PageSlug) && await repo.ExistsBySlugAsync(req.PageSlug))
            return ServiceResult<long>.Conflict("A document with this page slug already exists.");

        var newId = await repo.CreateAsync(req);
        return ServiceResult<long>.Ok(newId, "Document added successfully.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateDocumentRequest req)
    {
        if (!string.IsNullOrEmpty(req.PageSlug) && await repo.ExistsBySlugAsync(req.PageSlug, id))
            return ServiceResult.Conflict("A document with this page slug already exists.");

        return await repo.UpdateAsync(id, req)
            ? ServiceResult.Ok("Document updated.") : ServiceResult.NotFound("Document not found.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Document not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Document deleted.") : ServiceResult.NotFound("Document not found.");
    }

    public async Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload)
    {
        var fileName = await fileHelper.UploadImageAsync(upload, "documents");
        return ServiceResult<string>.Ok(fileHelper.GetFileUrl("documents", fileName));
    }
}
