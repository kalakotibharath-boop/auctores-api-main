using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class BannerService(IBannerRepository repo, FileUploadHelper fileHelper, IMapper mapper) : IBannerService
{
    public async Task<ServiceResult<IEnumerable<BannerDto>>> GetAllAsync()
        => ServiceResult<IEnumerable<BannerDto>>.Ok(await repo.GetAllAsync());

    public async Task<ServiceResult<long>> CreateAsync(CreateBannerRequest req, IFormFile? image)
    {
        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, "banners");
        var newId = await repo.CreateAsync(mapper.Map<BannerDto>(req), imgFile);
        return ServiceResult<long>.Ok(newId, "Banner added successfully.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Banner not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile("banners", img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Banner deleted.") : ServiceResult.NotFound("Banner not found.");
    }
}
