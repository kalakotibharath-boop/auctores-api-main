using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Models.Admin;
using AuctoresOnline.API.Models.Journal;
using AuctoresOnline.API.Models.Article;
using AuctoresOnline.API.Models.Editor;
using AuctoresOnline.API.Models.EditorProfile;
using AuctoresOnline.API.Models.Author;
using AuctoresOnline.API.Models.Reviewer;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Models.Document;
using AuctoresOnline.API.Models.Manuscript;
using AuctoresOnline.API.Models.JournalMisc;
using AuctoresOnline.API.Models.SpecialIssue;
using AuctoresOnline.API.Entities;

namespace AuctoresOnline.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    // Core tables
    public DbSet<AdminDto> Admins { get; set; }
    public DbSet<JournalDto> Journals { get; set; }
    public DbSet<JournalSectionDto> JournalSections { get; set; }
    public DbSet<JournalEditorialBoard> JournalEditorialBoards { get; set; }

    // Article tables
    public DbSet<ArticleDto> Articles { get; set; }
    public DbSet<ArticleDetailDto> ArticleDetails { get; set; }
    public DbSet<ArticleAuthorDto> ArticleAuthors { get; set; }
    public DbSet<ArticleReferenceDto> ArticleReferences { get; set; }
    public DbSet<TempCkeditorImage> TempCkeditorImages { get; set; }

    // People tables
    public DbSet<AuthorDto> Authors { get; set; }
    public DbSet<EditorDto> Editors { get; set; }
    public DbSet<EditorProfileDto> EditorProfiles { get; set; }
    public DbSet<ReviewerDto> Reviewers { get; set; }

    // Misc tables
    public DbSet<BannerDto> Banners { get; set; }
    public DbSet<CollaboratorDto> Collaborators { get; set; }
    public DbSet<ContactDto> Contacts { get; set; }
    public DbSet<TestimonialDto> Testimonials { get; set; }
    public DbSet<SubscriberDto> SubscriptionList { get; set; }
    public DbSet<DocumentDto> Documents { get; set; }

    // Manuscript tables
    public DbSet<ManuscriptRequestDto> ManuscriptRequests { get; set; }

    // Journal misc tables
    public DbSet<ApcDto> ArticleProcessingCharges { get; set; }
    public DbSet<AbstractingIndexingDto> AbstractingIndexing { get; set; }
    public DbSet<PubmedIndexDto> PubmedIndex { get; set; }
    public DbSet<MemberedInDto> MemberedIn { get; set; }
    public DbSet<BecomeRequestDto> BecomeRequests { get; set; }

    // Special issue tables
    public DbSet<SpecialIssueDto> SpecialIssues { get; set; }
    public DbSet<SpecialIssueEditor> SpecialIssueEditors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Exclude non-entity DTO types that are derived from entity types or used only as query projections
        modelBuilder.Ignore<JournalDetailDto>();
        modelBuilder.Ignore<JournalEditorDto>();
        modelBuilder.Ignore<EditorProfileBoardDto>();

        // Exclude all request/response-only types
        modelBuilder.Ignore<DashboardCountsDto>();
        modelBuilder.Ignore<LoginRequest>();
        modelBuilder.Ignore<LoginResponse>();
        modelBuilder.Ignore<UpdateProfileRequest>();
        modelBuilder.Ignore<ChangePasswordRequest>();
    }
}
