using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class ApcService(IApcRepository repo) : IApcService
{
    public async Task<ServiceResult<IEnumerable<ApcDto>>> GetAllAsync()
        => ServiceResult<IEnumerable<ApcDto>>.Ok(await repo.GetAllAsync());

    public async Task<ServiceResult<ApcDto>> GetByIdAsync(long id)
    {
        var apc = await repo.GetByIdAsync(id);
        if (apc is null) return ServiceResult<ApcDto>.NotFound("APC not found.");
        return ServiceResult<ApcDto>.Ok(apc);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateApcRequest req)
    {
        if (await repo.ExistsByJournalAsync(req.JournalId))
            return ServiceResult<long>.Conflict("An APC already exists for this journal.");
        var newId = await repo.CreateAsync(req);
        return ServiceResult<long>.Ok(newId, "APC created.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateApcRequest req)
    {
        if (await repo.ExistsByJournalAsync(req.JournalId, id))
            return ServiceResult.Conflict("An APC already exists for this journal.");
        return await repo.UpdateAsync(id, req)
            ? ServiceResult.Ok("APC updated.") : ServiceResult.NotFound("APC not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("APC deleted.") : ServiceResult.NotFound("APC not found.");
    }
}
