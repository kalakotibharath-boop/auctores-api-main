using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Reviewer;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class ReviewerService(IReviewerRepository repo, FileUploadHelper fileHelper, IMapper mapper) : IReviewerService
{
    private const string Folder = "reviewers";

    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<ReviewerDto>> GetByIdAsync(long id)
    {
        var reviewer = await repo.GetByIdAsync(id);
        if (reviewer is null) return ServiceResult<ReviewerDto>.NotFound("Reviewer not found.");
        return ServiceResult<ReviewerDto>.Ok(reviewer);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateReviewerRequest req, IFormFile? image)
    {
        if (!string.IsNullOrEmpty(req.ReviewerEmail) && await repo.ExistsByEmailAsync(req.ReviewerEmail))
            return ServiceResult<long>.Conflict("A reviewer with this email already exists.");

        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, Folder);

        var reviewer = mapper.Map<ReviewerDto>(req);
        var newId = await repo.CreateAsync(reviewer, imgFile);
        return ServiceResult<long>.Ok(newId, "Reviewer created.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateReviewerRequest req, IFormFile? image)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null) return ServiceResult.NotFound("Reviewer not found.");
        if (!string.IsNullOrEmpty(req.ReviewerEmail) && await repo.ExistsByEmailAsync(req.ReviewerEmail, id))
            return ServiceResult.Conflict("A reviewer with this email already exists.");

        mapper.Map(req, existing);

        string? imgFile = null;
        if (image != null)
        {
            if (!string.IsNullOrEmpty(existing.ReviewerImage)) fileHelper.DeleteFile(Folder, existing.ReviewerImage);
            imgFile = await fileHelper.UploadImageAsync(image, Folder);
        }

        await repo.UpdateAsync(id, existing, imgFile);
        return ServiceResult.Ok("Reviewer updated.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Reviewer not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile(Folder, img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Reviewer deleted.") : ServiceResult.NotFound("Reviewer not found.");
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
