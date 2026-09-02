using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Models.Document;
using AuctoresOnline.API.Models.Manuscript;
using AuctoresOnline.API.Models.JournalMisc;

namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IBannerRepository : IRepository<BannerDto>
{
    Task<IEnumerable<BannerDto>> GetAllAsync();
    Task<BannerDto?> GetByIdAsync(long id);
    Task<long> CreateAsync(BannerDto banner, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}

public interface ICollaboratorRepository : IRepository<CollaboratorDto>
{
    Task<IEnumerable<CollaboratorDto>> GetAllAsync();
    Task<CollaboratorDto?> GetByIdAsync(long id);
    Task<long> CreateAsync(CollaboratorDto collaborator, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}

public interface IContactRepository : IRepository<ContactDto>
{
    Task<IEnumerable<ContactDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<ContactDto?> GetByIdAsync(long id);
    Task<bool> UpdateStatusAsync(long id, string status);
    Task<bool> DeleteAsync(long id);
}

public interface ITestimonialRepository : IRepository<TestimonialDto>
{
    Task<IEnumerable<TestimonialDto>> GetAllAsync();
    Task<TestimonialDto?> GetByIdAsync(long id);
    Task<long> CreateAsync(TestimonialDto testimonial, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}

public interface ISubscriberRepository : IRepository<SubscriberDto>
{
    Task<IEnumerable<SubscriberDto>> GetAllAsync();
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
}

public interface IDocumentRepository : IRepository<DocumentDto>
{
    Task<IEnumerable<DocumentDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<DocumentDto?> GetByIdAsync(long id);
    Task<bool> ExistsBySlugAsync(string slug, long? excludeId = null);
    Task<long> CreateAsync(CreateDocumentRequest request);
    Task<bool> UpdateAsync(long id, CreateDocumentRequest request);
    Task<bool> UpdateStatusAsync(long id, int status);
    Task<bool> DeleteAsync(long id);
}

public interface IManuscriptRequestRepository : IRepository<ManuscriptRequestDto>
{
    Task<IEnumerable<ManuscriptRequestDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<ManuscriptRequestDto?> GetByIdAsync(long id);
    Task<bool> UpdateStatusAsync(long id, byte status);
    Task<bool> DeleteAsync(long id);
    Task<string?> GetFilesAsync(long id);
}

public interface IApcRepository : IRepository<ApcDto>
{
    Task<IEnumerable<ApcDto>> GetAllAsync();
    Task<ApcDto?> GetByIdAsync(long id);
    Task<bool> ExistsByJournalAsync(int journalId, long? excludeId = null);
    Task<long> CreateAsync(CreateApcRequest request);
    Task<bool> UpdateAsync(long id, CreateApcRequest request);
    Task<bool> DeleteAsync(long id);
}

public interface IAbstractingIndexingRepository : IRepository<AbstractingIndexingDto>
{
    Task<IEnumerable<AbstractingIndexingDto>> GetAllAsync();
    Task<AbstractingIndexingDto?> GetByIdAsync(long id);
    Task<bool> ExistsByJournalAndTitleAsync(int journalId, string title, long? excludeId = null);
    Task<long> CreateAsync(CreateAbstractingIndexingRequest request);
    Task<bool> UpdateAsync(long id, CreateAbstractingIndexingRequest request);
    Task<bool> DeleteAsync(long id);
}

public interface IPubmedIndexRepository : IRepository<PubmedIndexDto>
{
    Task<IEnumerable<PubmedIndexDto>> GetAllAsync();
    Task<PubmedIndexDto?> GetByIdAsync(long id);
    Task<bool> ExistsByJournalAndTextAsync(int journalId, string indexText, long? excludeId = null);
    Task<long> CreateAsync(CreatePubmedIndexRequest request);
    Task<bool> UpdateAsync(long id, CreatePubmedIndexRequest request);
    Task<bool> DeleteAsync(long id);
}

public interface IMemberedInRepository : IRepository<MemberedInDto>
{
    Task<IEnumerable<MemberedInDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<MemberedInDto?> GetByIdAsync(long id);
    Task<long> CreateAsync(CreateMemberedInRequest request, string? imageFileName);
    Task<bool> UpdateAsync(long id, CreateMemberedInRequest request, string? imageFileName);
    Task<bool> UpdateStatusAsync(long id, int status);
    Task<bool> DeleteAsync(long id);
    Task<string?> GetImageFileNameAsync(long id);
}

public interface IBecomeRequestRepository : IRepository<BecomeRequestDto>
{
    Task<IEnumerable<BecomeRequestDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<int> GetTotalCountAsync(string? search);
    Task<BecomeRequestDto?> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
}
