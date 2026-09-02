using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Author;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class AuthorService(IAuthorRepository repo, FileUploadHelper fileHelper, IMapper mapper) : IAuthorService
{
    private const string Folder = "authors";

    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<AuthorDto>> GetByIdAsync(long id)
    {
        var author = await repo.GetByIdAsync(id);
        if (author is null) return ServiceResult<AuthorDto>.NotFound("Author not found.");
        return ServiceResult<AuthorDto>.Ok(author);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateAuthorRequest req, IFormFile? image)
    {
        if (!string.IsNullOrEmpty(req.AuthorEmail) && await repo.ExistsByEmailAsync(req.AuthorEmail))
            return ServiceResult<long>.Conflict("An author with this email already exists.");

        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, Folder);

        var author = mapper.Map<AuthorDto>(req);
        var newId = await repo.CreateAsync(author, imgFile);
        return ServiceResult<long>.Ok(newId, "Author created.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateAuthorRequest req, IFormFile? image)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null) return ServiceResult.NotFound("Author not found.");
        if (!string.IsNullOrEmpty(req.AuthorEmail) && await repo.ExistsByEmailAsync(req.AuthorEmail, id))
            return ServiceResult.Conflict("An author with this email already exists.");

        mapper.Map(req, existing);

        string? imgFile = null;
        if (image != null)
        {
            if (!string.IsNullOrEmpty(existing.AuthorImage)) fileHelper.DeleteFile(Folder, existing.AuthorImage);
            imgFile = await fileHelper.UploadImageAsync(image, Folder);
        }

        await repo.UpdateAsync(id, existing, imgFile);
        return ServiceResult.Ok("Author updated.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Author not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile(Folder, img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Author deleted.") : ServiceResult.NotFound("Author not found.");
    }

    public async Task<ServiceResult> DeleteImageAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (string.IsNullOrEmpty(img)) return ServiceResult.Fail("No image to delete.");
        fileHelper.DeleteFile(Folder, img);
        await repo.ClearImageAsync(id);
        return ServiceResult.Ok("Image deleted.");
    }
}
