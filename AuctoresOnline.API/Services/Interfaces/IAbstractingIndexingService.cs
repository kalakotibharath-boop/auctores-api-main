using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IAbstractingIndexingService
{
    Task<ServiceResult<IEnumerable<AbstractingIndexingDto>>> GetAllAsync();
    Task<ServiceResult<AbstractingIndexingDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateAbstractingIndexingRequest req);
    Task<ServiceResult> UpdateAsync(long id, CreateAbstractingIndexingRequest req);
    Task<ServiceResult> DeleteAsync(long id);
}
