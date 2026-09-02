using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Models.Article;

[Table("Articles")]
public class ArticleDto
{
    [Key]
    public long ArticleId { get; set; }
    public int ArticleViews { get; set; }
    public int ArticleDownloads { get; set; }
    public string ArticleFor { get; set; } = "";
    public int JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public int VolumeNo { get; set; }
    public int IssueNo { get; set; }
    public byte ArticleType { get; set; }
    public string ArticleName { get; set; } = "";
    public string ArticleSeoName { get; set; } = "";
    public string? ArticleDoi { get; set; }
    public string CorrespondingAuthor { get; set; } = "";
    public DateTime? ReceivedDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? Citation { get; set; }
    public string? Copyright { get; set; }
    public string? ArticleInformation { get; set; }
    public string? ArticleAbstract { get; set; }
    public string? DisplayArticleInformation { get; set; }
    public string? DisplayArticleAbstract { get; set; }
    public string? AddressDesc { get; set; }
    public string? AbstractKeywords { get; set; }
    public string ArticlePdf { get; set; } = "";
    public byte ArticleStatus { get; set; }
    public DateTime ArticleCreatedDate { get; set; }
    public DateTime? ArticleUpdatedDate { get; set; }
}

[Table("ArticleDetails")]
public class ArticleDetailDto
{
    [Key]
    public long DetailsId { get; set; }
    public long DetailsArticleId { get; set; }
    public string DetailsHeading { get; set; } = "";
    public string? DetailsDescription { get; set; }
    public string? DisplayDetailsDescription { get; set; }
    public DateTime? CreatedDate { get; set; }
}

[Table("ArticleAuthors")]
public class ArticleAuthorDto
{
    [Key]
    public long ArticleAuthorId { get; set; }
    public long ArticleId { get; set; }
    public string AuthorName { get; set; } = "";
    public byte AuthorNo { get; set; }
    public string? AuthorSubhead { get; set; }
    public string? AuthorEmail { get; set; }
    public string? AuthorOrcid { get; set; }
    public string? AuthorAddress { get; set; }
}

[Table("ArticleReferences")]
public class ArticleReferenceDto
{
    [Key]
    public long ReferenceId { get; set; }
    public long ReferenceArticleId { get; set; }
    public string? ReferenceData { get; set; }
    public string? ReferenceGoogleLink { get; set; }
    public string? ReferencePublisherLink { get; set; }
}

public class ArticleAuthorRequest
{
    public string AuthorName { get; set; } = "";
    public byte AuthorNo { get; set; }
    public string? AuthorSubhead { get; set; }
    public string? AuthorEmail { get; set; }
    public string? AuthorOrcid { get; set; }
    public string? AuthorAddress { get; set; }
}

public class ArticleReferenceRequest
{
    public string? ReferenceData { get; set; }
    public string? ReferenceGoogleLink { get; set; }
    public string? ReferencePublisherLink { get; set; }
}

public class CreateArticleRequest
{
    public string ArticleFor { get; set; } = "";
    public int JournalId { get; set; }
    public int VolumeNo { get; set; }
    public int IssueNo { get; set; }
    public byte ArticleType { get; set; }
    public string ArticleName { get; set; } = "";
    public string ArticleSeoName { get; set; } = "";
    public string? ArticleDoi { get; set; }
    public string CorrespondingAuthor { get; set; } = "";
    public DateTime? ReceivedDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? Citation { get; set; }
    public string? Copyright { get; set; }
    public string? ArticleInformation { get; set; }
    public string? ArticleAbstract { get; set; }
    public string? DisplayArticleInformation { get; set; }
    public string? DisplayArticleAbstract { get; set; }
    public string? AddressDesc { get; set; }
    public string? AbstractKeywords { get; set; }
    public List<ArticleAuthorRequest> Authors { get; set; } = [];
    public List<ArticleReferenceRequest> References { get; set; } = [];
}

public class SaveArticleDetailRequest
{
    public long? DetailsId { get; set; }
    public string DetailsHeading { get; set; } = "";
    public string? DetailsDescription { get; set; }
    public string? DisplayDetailsDescription { get; set; }
}

public record ChangeArticleTypeRequest(byte ArticleType);
