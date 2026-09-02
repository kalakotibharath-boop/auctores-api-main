using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Journal;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class JournalService(IJournalRepository repo, FileUploadHelper fileHelper, IMapper mapper) : IJournalService
{
    public async Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search)
    {
        var items = await repo.GetAllAsync(page, pageSize, search);
        var total = await repo.GetTotalCountAsync(search);
        return ServiceResult<object>.Ok(new { data = items, total, page, pageSize });
    }

    public async Task<ServiceResult<JournalDetailDto>> GetByIdAsync(int id)
    {
        var journal = await repo.GetDetailsByIdAsync(id);
        if (journal is null) return ServiceResult<JournalDetailDto>.NotFound("Journal not found.");
        return ServiceResult<JournalDetailDto>.Ok(journal);
    }

    public async Task<ServiceResult<int>> CreateAsync(CreateJournalRequest req, IFormFile? journalImage, IFormFile? indexingImage, IFormFile? crossrefImage)
    {
        if (await repo.ExistsByNameAsync(req.JournalName))
            return ServiceResult<int>.Conflict("A journal with this name already exists.");

        string? jImg = null, iImg = null, cImg = null;
        if (journalImage != null) jImg = await fileHelper.UploadImageAsync(journalImage, "journals");
        if (indexingImage != null) iImg = await fileHelper.UploadImageAsync(indexingImage, "journals");
        if (crossrefImage != null) cImg = await fileHelper.UploadImageAsync(crossrefImage, "journals");

        var newId = await repo.CreateAsync(mapper.Map<JournalDto>(req), jImg, iImg, cImg);
        await repo.ReplaceEditorialBoardAsync(newId, BuildRoleEditors(req));
        await repo.ReplaceSectionsAsync(newId, req.Sections);
        return ServiceResult<int>.Ok(newId, "Journal created successfully.");
    }

    public async Task<ServiceResult> UpdateAsync(int id, CreateJournalRequest req, IFormFile? journalImage, IFormFile? indexingImage, IFormFile? crossrefImage)
    {
        if (await repo.GetByIdAsync(id) is null) return ServiceResult.NotFound("Journal not found.");
        if (await repo.ExistsByNameAsync(req.JournalName, id)) return ServiceResult.Conflict("A journal with this name already exists.");

        string? jImg = null, iImg = null, cImg = null;
        if (journalImage != null) jImg = await fileHelper.UploadImageAsync(journalImage, "journals");
        if (indexingImage != null) iImg = await fileHelper.UploadImageAsync(indexingImage, "journals");
        if (crossrefImage != null) cImg = await fileHelper.UploadImageAsync(crossrefImage, "journals");

        await repo.UpdateAsync(id, mapper.Map<JournalDto>(req), jImg, iImg, cImg);
        await repo.ReplaceEditorialBoardAsync(id, BuildRoleEditors(req));
        await repo.ReplaceSectionsAsync(id, req.Sections);
        return ServiceResult.Ok("Journal updated successfully.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(int id, int currentStatus)
    {
        int newStatus = (currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Journal not found.");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Journal deleted.") : ServiceResult.NotFound("Journal not found.");
    }

    public async Task<ServiceResult<string>> UploadCkEditorImageAsync(IFormFile upload)
    {
        var fileName = await fileHelper.UploadImageAsync(upload, "journals/about");
        return ServiceResult<string>.Ok(fileHelper.GetFileUrl("journals/about", fileName));
    }

    private static Dictionary<string, List<long>> BuildRoleEditors(CreateJournalRequest req) => new()
    {
        ["Manuscript Editor"] = req.ManuscriptEditorIds,
        ["Assistant Editor"] = req.AssistantEditorIds,
        ["Consulting Editor"] = req.ConsultingEditorIds,
        ["Associate Editor"] = req.AssociateEditorIds,
        ["Editor Profiles"] = req.EditorProfileIds
    };
}
