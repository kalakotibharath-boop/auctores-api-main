using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Manuscript;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class ManuscriptService(IManuscriptRequestRepository repo, FileUploadHelper fileHelper) : IManuscriptService
{
    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<ManuscriptRequestDto>> GetByIdAsync(long id)
    {
        var manuscript = await repo.GetByIdAsync(id);
        if (manuscript is null) return ServiceResult<ManuscriptRequestDto>.NotFound("Manuscript request not found.");
        return ServiceResult<ManuscriptRequestDto>.Ok(manuscript);
    }

    public async Task<ServiceResult> UpdateStatusAsync(long id, ChangeManuscriptStatusRequest req)
    {
        return await repo.UpdateStatusAsync(id, req.Status)
            ? ServiceResult.Ok("Status updated.") : ServiceResult.NotFound("Manuscript request not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var files = await repo.GetFilesAsync(id);
        if (!string.IsNullOrEmpty(files))
        {
            foreach (var file in files.Split(',', StringSplitOptions.RemoveEmptyEntries))
                fileHelper.DeleteFile("manuscripts", file.Trim());
        }
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Manuscript request deleted.") : ServiceResult.NotFound("Not found.");
    }
}
