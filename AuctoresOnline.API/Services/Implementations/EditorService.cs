using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Editor;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class EditorService(IEditorRepository repo, FileUploadHelper fileHelper, IMapper mapper) : IEditorService
{
    private const string Folder = "editors";

    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<EditorDto>> GetByIdAsync(long id)
    {
        var editor = await repo.GetByIdAsync(id);
        if (editor is null) return ServiceResult<EditorDto>.NotFound("Editor not found.");
        return ServiceResult<EditorDto>.Ok(editor);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateEditorRequest req, IFormFile? image)
    {
        if (!string.IsNullOrEmpty(req.EditorEmail) && await repo.ExistsByEmailAsync(req.EditorEmail))
            return ServiceResult<long>.Conflict("An editor with this email already exists.");

        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, Folder);

        var editor = mapper.Map<EditorDto>(req);
        var newId = await repo.CreateAsync(editor, imgFile);
        return ServiceResult<long>.Ok(newId, "Editor created successfully.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateEditorRequest req, IFormFile? image)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null) return ServiceResult.NotFound("Editor not found.");
        if (!string.IsNullOrEmpty(req.EditorEmail) && await repo.ExistsByEmailAsync(req.EditorEmail, id))
            return ServiceResult.Conflict("An editor with this email already exists.");

        mapper.Map(req, existing);

        string? imgFile = null;
        if (image != null)
        {
            if (!string.IsNullOrEmpty(existing.ProfileImage)) fileHelper.DeleteFile(Folder, existing.ProfileImage);
            imgFile = await fileHelper.UploadImageAsync(image, Folder);
        }

        await repo.UpdateAsync(id, existing, imgFile);
        return ServiceResult.Ok("Editor updated successfully.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Editor not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile(Folder, img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Editor deleted.") : ServiceResult.NotFound("Editor not found.");
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
