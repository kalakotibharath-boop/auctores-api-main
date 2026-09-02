using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Services.Interfaces;

namespace AuctoresOnline.API.Services.Implementations;

public class TestimonialService(ITestimonialRepository repo, FileUploadHelper fileHelper, IMapper mapper) : ITestimonialService
{
    public async Task<ServiceResult<IEnumerable<TestimonialDto>>> GetAllAsync()
        => ServiceResult<IEnumerable<TestimonialDto>>.Ok(await repo.GetAllAsync());

    public async Task<ServiceResult<long>> CreateAsync(CreateTestimonialRequest req, IFormFile? image)
    {
        string? imgFile = null;
        if (image != null) imgFile = await fileHelper.UploadImageAsync(image, "testimonials");
        var newId = await repo.CreateAsync(mapper.Map<TestimonialDto>(req), imgFile);
        return ServiceResult<long>.Ok(newId, "Testimonial added successfully.");
    }

    public async Task<ServiceResult> ToggleStatusAsync(long id, int currentStatus)
    {
        byte newStatus = (byte)(currentStatus == 1 ? 0 : 1);
        return await repo.UpdateStatusAsync(id, newStatus)
            ? ServiceResult.Ok() : ServiceResult.NotFound("Testimonial not found.");
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var img = await repo.GetImageFileNameAsync(id);
        if (!string.IsNullOrEmpty(img)) fileHelper.DeleteFile("testimonials", img);
        return await repo.DeleteAsync(id)
            ? ServiceResult.Ok("Testimonial deleted.") : ServiceResult.NotFound("Testimonial not found.");
    }
}
