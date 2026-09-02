using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class CollaboratorService(ICollaboratorRepository repo, FileUploadHelper fileHelper, IMapper mapper) : ICollaboratorService
{
    public async Task<ServiceResult<IEnumerable<CollaboratorDto>>> GetAllAsync()
        => ServiceResult<IEnumerable<CollaboratorDto>>.Ok(await repo.GetAllAsync());

    public async Task<ServiceResult<long>> CreateAsync(CreateCollaboratorRequest req, IFormFile? image)
    {
        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, "collborators");
        var newId = await repo.CreateAsync(mapper.Map<CollaboratorDto>(req), imgFile);
        return ServiceResult<long>.Ok(newId, "Collaborator added successfully.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Collaborator not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile("collborators", img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Collaborator deleted.") : ServiceResult.NotFound("Collaborator not found.");
    }
}
