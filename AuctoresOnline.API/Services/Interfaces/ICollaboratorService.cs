using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface ICollaboratorService
{
    Task<ServiceResult<IEnumerable<CollaboratorDto>>> GetAllAsync();
    Task<ServiceResult<long>> CreateAsync(CreateCollaboratorRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
}
