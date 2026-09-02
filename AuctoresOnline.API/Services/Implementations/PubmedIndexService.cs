using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class PubmedIndexService(IPubmedIndexRepository repo) : IPubmedIndexService
{
    public async Task<ServiceResult<IEnumerable<PubmedIndexDto>>> GetAllAsync()
        => ServiceResult<IEnumerable<PubmedIndexDto>>.Ok(await repo.GetAllAsync());

    public async Task<ServiceResult<PubmedIndexDto>> GetByIdAsync(long id)
    {
        var item = await repo.GetByIdAsync(id);
        if (item is null) return ServiceResult<PubmedIndexDto>.NotFound("Pubmed index entry not found.");
        return ServiceResult<PubmedIndexDto>.Ok(item);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreatePubmedIndexRequest req)
    {
        if (!string.IsNullOrEmpty(req.IndexText) && await repo.ExistsByJournalAndTextAsync(req.JournalId, req.IndexText))
            return ServiceResult<long>.Conflict("An entry with this text already exists for this journal.");
        var newId = await repo.CreateAsync(req);
        return ServiceResult<long>.Ok(newId, "Pubmed index added.");
    }

    public async Task<ServiceResult> UpdateAsync(long id, CreatePubmedIndexRequest req)
    {
        if (!string.IsNullOrEmpty(req.IndexText) && await repo.ExistsByJournalAndTextAsync(req.JournalId, req.IndexText, id))
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
