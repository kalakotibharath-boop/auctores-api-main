using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IContactService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<ContactDto>> GetByIdAsync(long id);
    Task<ServiceResult> UpdateStatusAsync(long id, UpdateContactStatusRequest req);
    Task<ServiceResult> DeleteAsync(long id);
}
