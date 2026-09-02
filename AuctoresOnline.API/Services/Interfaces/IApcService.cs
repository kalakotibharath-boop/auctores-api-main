using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.JournalMisc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IApcService
{
    Task<ServiceResult<IEnumerable<ApcDto>>> GetAllAsync();
    Task<ServiceResult<ApcDto>> GetByIdAsync(long id);
    Task<ServiceResult<long>> CreateAsync(CreateApcRequest req);
    Task<ServiceResult> UpdateAsync(long id, CreateApcRequest req);
    Task<ServiceResult> DeleteAsync(long id);
}
