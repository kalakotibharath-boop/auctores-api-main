using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;

namespace AuctoresOnline.API.Services.Interfaces.User;

public interface IUserDocumentService
{
    Task<ServiceResult<IEnumerable<DocumentPublicDto>>> GetAllDocumentsAsync();
    Task<ServiceResult<DocumentPublicDetailDto>> GetDocumentBySlugAsync(string slug);
}
