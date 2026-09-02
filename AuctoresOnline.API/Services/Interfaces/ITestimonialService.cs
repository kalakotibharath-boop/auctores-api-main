using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;

namespace AuctoresOnline.API.Services.Interfaces;

public interface ITestimonialService
{
    Task<ServiceResult<IEnumerable<TestimonialDto>>> GetAllAsync();
    Task<ServiceResult<long>> CreateAsync(CreateTestimonialRequest req, IFormFile? image);
    Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus);
    Task<ServiceResult> DeleteAsync(long id);
}
