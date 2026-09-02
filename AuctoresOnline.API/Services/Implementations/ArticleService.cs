using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Article;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class ArticleService(IArticleRepository repo, FileUploadHelper fileHelper, IMapper mapper) : IArticleService
{
    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search, byte? articleType)
    {
        var items = await repo.GetAllAsync(page, pageSize, search, articleType);
        var total = await repo.GetTotalCountAsync(search, articleType);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<object>> GetByIdAsync(long id)
    {
        var article = await repo.GetByIdAsync(id);
        if (article is null) return ServiceResult<object>.NotFound("Article not found.");
        var details = await repo.GetDetailsAsync(id);
        return ServiceResult<object>.Ok(new { article, details });
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateArticleRequest req, IFormFile? pdf)
    {
        if (pdf is null) return ServiceResult<long>.Fail("Article PDF is required.");
        if (await repo.ExistsByNameAndJournalAsync(req.ArticleName, req.JournalId))
            return ServiceResult<long>.Conflict("An article with this name already exists for this journal.");

        var pdfName = await fileHelper.UploadPdfAsync(pdf, "articles");
        var newId = await repo.CreateAsync(mapper.Map<ArticleDto>(req), pdfName);
        await repo.ReplaceAuthorsAsync(newId, req.Authors);
        await repo.ReplaceReferencesAsync(newId, req.References);
        return ServiceResult<long>.Ok(newId, "Article created successfully.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateArticleRequest req, IFormFile? pdf)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null) return ServiceResult.NotFound("Article not found.");
        if (await repo.ExistsByNameAndJournalAsync(req.ArticleName, req.JournalId, id))
            return ServiceResult.Conflict("An article with this name already exists for this journal.");

        string? newPdf = null;
        if (pdf != null)
        {
            if (!string.IsNullOrEmpty(existing.ArticlePdf)) fileHelper.DeleteFile("articles", existing.ArticlePdf);
            newPdf = await fileHelper.UploadPdfAsync(pdf, "articles");
            await repo.UpdateCkEditorImagesAsync(id, "article_information");
            await repo.UpdateCkEditorImagesAsync(id, "article_abstract");
        }

        await repo.UpdateAsync(id, mapper.Map<ArticleDto>(req), newPdf);
        await repo.ReplaceAuthorsAsync(id, req.Authors);
        await repo.ReplaceReferencesAsync(id, req.References);
        return ServiceResult.Ok("Article updated successfully.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Article not found.");
    }

    public async Task<ServiceResult> ChangeTypeAsync(long id, ChangeArticleTypeRequest req)
    {
        return await repo.UpdateTypeAsync(id, req.ArticleType)
            ? ServiceResult.Ok("Article type updated.") : ServiceResult.NotFound("Article not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var article = await repo.GetByIdAsync(id);
        if (article is null) return ServiceResult.NotFound("Article not found.");
        if (!string.IsNullOrEmpty(article.ArticlePdf)) fileHelper.DeleteFile("articles", article.ArticlePdf);
        await repo.DeleteAsync(id);
        return ServiceResult.Ok("Article deleted.");
    }

    public async Task<ServiceResult<IEnumerable<ArticleDetailDto>>> GetDetailsAsync(long articleId)
    {
        return ServiceResult<IEnumerable<ArticleDetailDto>>.Ok(await repo.GetDetailsAsync(articleId));
    }

    public async Task<ServiceResult<long>> SaveDetailAsync(long articleId, SaveArticleDetailRequest req)
    {
        var id = await repo.SaveDetailAsync(articleId, req);
        return ServiceResult<long>.Ok(id, "Detail saved.");
    }

    public async Task<ServiceResult> DeleteDetailAsync(long detailsId)
    {
        return await repo.DeleteDetailAsync(detailsId)
            ? ServiceResult.Ok("Detail deleted.") : ServiceResult.NotFound("Detail not found.");
    }

    public async Task<ServiceResult<ArticleDetailDto>> GetDetailByIdAsync(long detailsId)
    {
        var detail = await repo.GetDetailByIdAsync(detailsId);
        if (detail is null) return ServiceResult<ArticleDetailDto>.NotFound("Detail not found.");
        return ServiceResult<ArticleDetailDto>.Ok(detail);
    }

    public async Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload)
    {
        var fileName = await fileHelper.UploadImageAsync(upload, "articles");
        return ServiceResult<string>.Ok(fileHelper.GetFileUrl("articles", fileName));
    }

}
