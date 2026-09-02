using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Manuscript;

namespace AuctoresOnline.API.Services.Interfaces;

public interface IManuscriptService
{
    Task<ServiceResult<object>> GetAllAsync(int page, int pageSize, string? search);
    Task<ServiceResult<ManuscriptRequestDto>> GetByIdAsync(long id);
    Task<ServiceResult> UpdateStatusAsync(long id, ChangeManuscriptStatusRequest req);
    Task<ServiceResult> DeleteAsync(long id);
}
