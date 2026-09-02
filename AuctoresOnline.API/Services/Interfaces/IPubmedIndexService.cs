using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IPubmedIndexService
{
    Task<ServiceResult<IEnumerable<PubmedIndexDto>>> GetAllAsync();
    Task<ServiceResult<PubmedIndexDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreatePubmedIndexRequest req);
    Task<ServiceResult> UpdateAsync(long id, CreatePubmedIndexRequest req);
    Task<ServiceResult> DeleteAsync(long id);
}
