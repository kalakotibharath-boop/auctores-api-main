namespace AuctoresOnline.API.Models.User;

// ── Home ──────────────────────────────────────────────────────────────────────

public class HomeDataDto
{
    public int JournalsCount { get; set; }
    public int AdvisoryBoardCount { get; set; }
    public IEnumerable<ArticleListItemDto> RecentArticles { get; set; } = [];
    public IEnumerable<CollaboratorPublicDto> Collaborators { get; set; } = [];
}

// ── Journal Listing ───────────────────────────────────────────────────────────

public class JournalListItemDto
{
    public string JournalName { get; set; } = "";
    public string? JournalSeoName { get; set; }
    public bool IsExternalJournal { get; set; }
}

public class JournalListingDto
{
    public IEnumerable<string> StartingLetters { get; set; } = [];
    public Dictionary<string, List<JournalListItemDto>> Journals { get; set; } = [];
    public IEnumerable<TestimonialPublicDto> Testimonials { get; set; } = [];
}

// ── Journal Detail ────────────────────────────────────────────────────────────

public class JournalPublicDetailDto
{
    public int JournalId { get; set; }
    public string JournalName { get; set; } = "";
    public string? JournalSeoName { get; set; }
    public string? JournalDoi { get; set; }
    public string? IssnNumber { get; set; }
    public string? JournalApc { get; set; }
    public string? JournalImpact { get; set; }
    public string? ImpactFactor { get; set; }
    public string? CallForPapers { get; set; }
    public string? AimsAndScope { get; set; }
    public string? JournalImage { get; set; }
    public int Volume { get; set; }
    public int Issue { get; set; }
    public int Year { get; set; }
    public string? Email { get; set; }
    public string? YoutubeLink { get; set; }
    public string? JournalAcceptanceRate { get; set; }
    public string? JournalCitescore { get; set; }
    public string? CrossrefUrl { get; set; }
    public string? IndexingUrl { get; set; }
    public bool IsExternalJournal { get; set; }
    public IEnumerable<JournalSectionPublicDto> Sections { get; set; } = [];
    public IEnumerable<ArticleListItemDto> RecentArticles { get; set; } = [];
    public IEnumerable<PubmedIndexPublicDto> PubmedIndex { get; set; } = [];
    public IEnumerable<MemberedInPublicDto> Members { get; set; } = [];
}

public class JournalSectionPublicDto
{
    public int SectionId { get; set; }
    public string SectionName { get; set; } = "";
}

// ── Editorial Board ───────────────────────────────────────────────────────────

public class EditorialBoardDto
{
    public int JournalId { get; set; }
    public string? JournalName { get; set; }
    public string? JournalImage { get; set; }
    public IEnumerable<string> Roles { get; set; } = [];
    public IEnumerable<EditorBoardMemberDto> Members { get; set; } = [];
}

public class EditorBoardMemberDto
{
    public long EditorId { get; set; }
    public string EditorName { get; set; } = "";
    public string? EditorDesignation { get; set; }
    public string? ProfileImage { get; set; }
    public string EditorRole { get; set; } = "";
}

// ── Editor Profiles ───────────────────────────────────────────────────────────

public class EditorProfilesPublicDto
{
    public int JournalId { get; set; }
    public string? JournalName { get; set; }
    public string? JournalImage { get; set; }
    public IEnumerable<EditorProfileMemberDto> Members { get; set; } = [];
}

public class EditorProfileMemberDto
{
    public long EditorId { get; set; }
    public string EditorName { get; set; } = "";
    public string? EditorDesignation { get; set; }
    public string? EditorImage { get; set; }
    public string? EditorDescription { get; set; }
}

// ── Articles ──────────────────────────────────────────────────────────────────

public class ArticleListItemDto
{
    public long ArticleId { get; set; }
    public string ArticleName { get; set; } = "";
    public string ArticleSeoName { get; set; } = "";
    public string? ArticlePdf { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string ArticleFor { get; set; } = "";
    public int VolumeNo { get; set; }
    public int IssueNo { get; set; }
    public string? AllAuthorNames { get; set; }
}

public class ArticlePublicDetailDto
{
    public long ArticleId { get; set; }
    public string ArticleName { get; set; } = "";
    public string ArticleSeoName { get; set; } = "";
    public string? ArticleDoi { get; set; }
    public string? ArticleAbstract { get; set; }
    public string? DisplayArticleAbstract { get; set; }
    public string? ArticleInformation { get; set; }
    public string? DisplayArticleInformation { get; set; }
    public string? AbstractKeywords { get; set; }
    public string? ArticlePdf { get; set; }
    public string CorrespondingAuthor { get; set; } = "";
    public DateTime? ReceivedDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? Citation { get; set; }
    public string? Copyright { get; set; }
    public string ArticleFor { get; set; } = "";
    public int VolumeNo { get; set; }
    public int IssueNo { get; set; }
    public int JournalId { get; set; }
    public string? JournalName { get; set; }
    public string? JournalSeoName { get; set; }
    public int ArticleViews { get; set; }
    public int ArticleDownloads { get; set; }
    public IEnumerable<ArticleAuthorPublicDto> Authors { get; set; } = [];
    public IEnumerable<ArticleDetailPublicDto> Details { get; set; } = [];
    public IEnumerable<ArticleReferencePublicDto> References { get; set; } = [];
}

public class ArticleAuthorPublicDto
{
    public string AuthorName { get; set; } = "";
    public byte AuthorNo { get; set; }
    public string? AuthorSubhead { get; set; }
    public string? AuthorEmail { get; set; }
    public string? AuthorOrcid { get; set; }
    public string? AuthorAddress { get; set; }
}

public class ArticleDetailPublicDto
{
    public long DetailsId { get; set; }
    public string DetailsHeading { get; set; } = "";
    public string? DisplayDetailsDescription { get; set; }
}

public class ArticleReferencePublicDto
{
    public long ReferenceId { get; set; }
    public string? ReferenceData { get; set; }
    public string? ReferenceGoogleLink { get; set; }
    public string? ReferencePublisherLink { get; set; }
}

// ── Archives ──────────────────────────────────────────────────────────────────

public class ArchiveVolumeDto
{
    public int VolumeNo { get; set; }
    public int PublishedYear { get; set; }
}

public class ArchiveIssueDto
{
    public int VolumeNo { get; set; }
    public int IssueNo { get; set; }
    public int PublishedYear { get; set; }
    public string PublishedMonth { get; set; } = "";
}

public class ArchiveDataDto
{
    public IEnumerable<ArchiveVolumeDto> Volumes { get; set; } = [];
    public IEnumerable<ArchiveIssueDto> Issues { get; set; } = [];
}

// ── Shared Public DTOs ────────────────────────────────────────────────────────

public class CollaboratorPublicDto
{
    public long CollaboratorId { get; set; }
    public string? CollaboratorName { get; set; }
    public string? CollaboratorUrl { get; set; }
    public string? CollaboratorImage { get; set; }
}

public class TestimonialPublicDto
{
    public long TestimonialId { get; set; }
    public string? TestimonialName { get; set; }
    public string? TestimonialImage { get; set; }
    public string? TestimonialDescription { get; set; }
}

public class PubmedIndexPublicDto
{
    public int PubmedIndexId { get; set; }
    public string? IndexText { get; set; }
    public string? PubmedUrl { get; set; }
    public string? PmcUrl { get; set; }
}

public class MemberedInPublicDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Url { get; set; }
}

public class AbstractingIndexingPublicDto
{
    public long AbstractionId { get; set; }
    public string AbstractionTitle { get; set; } = "";
    public string AbstractionUrl { get; set; } = "";
}

public class JournalSubmitOptionDto
{
    public int JournalId { get; set; }
    public string JournalName { get; set; } = "";
    public string? JournalSeoName { get; set; }
}

// ── Special Issue ─────────────────────────────────────────────────────────────

public class SpecialIssueListItemDto
{
    public long IssueId { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public string? Keywords { get; set; }
}

public class SpecialIssuePublicDetailDto
{
    public long IssueId { get; set; }
    public string? Title { get; set; }
    public long JournalId { get; set; }
    public string? JournalName { get; set; }
    public string? JournalSeoName { get; set; }
    public string? Slug { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public int ViewCount { get; set; }
    public string? IssnNumber { get; set; }
    public string? IssueDataDisplay { get; set; }
    public string? Keywords { get; set; }
    public string? Benefits { get; set; }
    public string? FileLink { get; set; }
    public IEnumerable<SpecialIssueEditorPublicDto> Editors { get; set; } = [];
}

public class SpecialIssueEditorPublicDto
{
    public long EditorId { get; set; }
    public string EditorName { get; set; } = "";
    public string? EditorDesignation { get; set; }
    public string? EditorImage { get; set; }
}

// ── Document Pages ────────────────────────────────────────────────────────────

public class DocumentPublicDto
{
    public long DocId { get; set; }
    public string? Title { get; set; }
    public string? PageSlug { get; set; }
    public string? SeoKeywords { get; set; }
}

public class DocumentPublicDetailDto
{
    public long DocId { get; set; }
    public string? Title { get; set; }
    public string? PageSlug { get; set; }
    public string? DocData { get; set; }
    public string? SeoKeywords { get; set; }
    public IEnumerable<DocumentPublicDto> AllPages { get; set; } = [];
}

// ── Article Processing Charges ────────────────────────────────────────────────

public class ApcPublicDto
{
    public int JournalId { get; set; }
    public string? JournalName { get; set; }
    public string? JournalSeoName { get; set; }
    public int ApcAmount { get; set; }
}

// ── Submission Requests ───────────────────────────────────────────────────────

public class SubscribeRequest
{
    public string Email { get; set; } = "";
    public string? SubscribedFor { get; set; }
    public long? SubscribedForId { get; set; }
}

public class ContactUsRequest
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Phone { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
}

public class BecomeMemberRequest
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string? PhoneNumber { get; set; }
    public string? Designation { get; set; }
    public string? Country { get; set; }
    public string? BioData { get; set; }
    public int JournalId { get; set; }
    public int Type { get; set; }
}

public class SubmitManuscriptRequest
{
    public string? AFname { get; set; }
    public string? ALname { get; set; }
    public string? AEmail { get; set; }
    public string? APhone { get; set; }
    public string? ACountry { get; set; }
    public string? AOrcid { get; set; }
    public string? AAddress { get; set; }
    public string? MTitle { get; set; }
    public long? JournalId { get; set; }
    public string? MArticleType { get; set; }
    public string? MAbstract { get; set; }
    public string? MKeywords { get; set; }
    public string? MCoverLetter { get; set; }
}
