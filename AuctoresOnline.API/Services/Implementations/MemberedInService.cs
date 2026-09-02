using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class MemberedInService(IMemberedInRepository repo, FileUploadHelper fileHelper) : IMemberedInService
{
    private const string Folder = "membered_in";

    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<MemberedInDto>> GetByIdAsync(long id)
    {
        var item = await repo.GetByIdAsync(id);
        if (item is null) return ServiceResult<MemberedInDto>.NotFound("Entry not found.");
        return ServiceResult<MemberedInDto>.Ok(item);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateMemberedInRequest req, IFormFile? image)
    {
        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, Folder);
        var newId = await repo.CreateAsync(req, imgFile);
        return ServiceResult<long>.Ok(newId, "Membered-in added.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateMemberedInRequest req, IFormFile? image)
    {
        string? imgFile = null;
        if (image != null)
        {
            var old = await repo.GetImageFileNameAsync(id);
            if (!string.IsNullOrEmpty(old)) fileHelper.DeleteFile(Folder, old);
            imgFile = await fileHelper.UploadImageAsync(image, Folder);
        }
        return await repo.UpdateAsync(id, req, imgFile)
            ? ServiceResult.Ok("Updated.") : ServiceResult.NotFound("Entry not found.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Entry not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile(Folder, img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Deleted.") : ServiceResult.NotFound("Entry not found.");
    }
}
