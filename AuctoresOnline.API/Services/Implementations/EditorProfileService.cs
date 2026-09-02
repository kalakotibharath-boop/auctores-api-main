using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.EditorProfile;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class EditorProfileService(IEditorProfileRepository repo, FileUploadHelper fileHelper, IMapper mapper) : IEditorProfileService
{
    private const string Folder = "editor_profiles";

    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<EditorProfileDto>> GetByIdAsync(long id)
    {
        var profile = await repo.GetByIdAsync(id);
        if (profile is null) return ServiceResult<EditorProfileDto>.NotFound("Editor profile not found.");
        return ServiceResult<EditorProfileDto>.Ok(profile);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateEditorProfileRequest req, IFormFile? image)
    {
        if (!string.IsNullOrEmpty(req.EditorEmail) && await repo.ExistsByEmailAsync(req.EditorEmail))
            return ServiceResult<long>.Conflict("An editor profile with this email already exists.");

        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, Folder);

        var profile = mapper.Map<EditorProfileDto>(req);
        var newId = await repo.CreateAsync(profile, imgFile);
        return ServiceResult<long>.Ok(newId, "Editor profile created.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateEditorProfileRequest req, IFormFile? image)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null) return ServiceResult.NotFound("Editor profile not found.");
        if (!string.IsNullOrEmpty(req.EditorEmail) && await repo.ExistsByEmailAsync(req.EditorEmail, id))
            return ServiceResult.Conflict("An editor profile with this email already exists.");

        mapper.Map(req, existing);

        string? imgFile = null;
        if (image != null)
        {
            if (!string.IsNullOrEmpty(existing.EditorImage)) fileHelper.DeleteFile(Folder, existing.EditorImage);
            imgFile = await fileHelper.UploadImageAsync(image, Folder);
        }

        await repo.UpdateAsync(id, existing, imgFile);
        return ServiceResult.Ok("Editor profile updated.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Editor profile not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile(Folder, img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Editor profile deleted.") : ServiceResult.NotFound("Editor profile not found.");
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
