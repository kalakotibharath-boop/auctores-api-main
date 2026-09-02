using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class AbstractingIndexingService(IAbstractingIndexingRepository repo) : IAbstractingIndexingService
{
    public async Task<ServiceResult<IEnumerable<AbstractingIndexingDto>>> GetAllAsync()
        => ServiceResult<IEnumerable<AbstractingIndexingDto>>.Ok(await repo.GetAllAsync());

    public async Task<ServiceResult<AbstractingIndexingDto>> GetByIdAsync(long id)
    {
        var item = await repo.GetByIdAsync(id);
        if (item is null) return ServiceResult<AbstractingIndexingDto>.NotFound("Entry not found.");
        return ServiceResult<AbstractingIndexingDto>.Ok(item);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateAbstractingIndexingRequest req)
    {
        if (await repo.ExistsByJournalAndTitleAsync(req.JournalId, req.AbstractionTitle))
            return ServiceResult<long>.Conflict("An entry with this title already exists for this journal.");
        var newId = await repo.CreateAsync(req);
        return ServiceResult<long>.Ok(newId, "Abstracting indexing added.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreateAbstractingIndexingRequest req)
    {
        if (await repo.ExistsByJournalAndTitleAsync(req.JournalId, req.AbstractionTitle, id))
            return ServiceResult.Conflict("An entry already exists.");
        return await repo.UpdateAsync(id, req)
            ? ServiceResult.Ok("Updated.") : ServiceResult.NotFound("Entry not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Deleted.") : ServiceResult.NotFound("Entry not found.");
    }
}
