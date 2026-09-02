using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Models.JournalMisc;

[Table("ArticleProcessingCharges")]
public class ApcDto
{
    [Key]
    public long ApcId { get; set; }
    public int JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public int ApcAmount { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public record CreateApcRequest(int JournalId, int ApcAmount);

[Table("AbstractingIndexing")]
public class AbstractingIndexingDto
{
    [Key]
    public long AbstractionId { get; set; }
    public int JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public string AbstractionUrl { get; set; } = "";
    public string AbstractionTitle { get; set; } = "";
    public byte Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public record CreateAbstractingIndexingRequest(int JournalId, string AbstractionUrl, string AbstractionTitle);

[Table("PubmedIndex")]
public class PubmedIndexDto
{
    [Key]
    public int PubmedIndexId { get; set; }
    public int JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public string? IndexText { get; set; }
    public string? PubmedUrl { get; set; }
    public string? PmcUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public record CreatePubmedIndexRequest(int JournalId, string? IndexText, string? PubmedUrl, string? PmcUrl);

[Table("MemberedIn")]
public class MemberedInDto
{
    [Key]
    public long Id { get; set; }
    public int JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Url { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public class CreateMemberedInRequest
{
    public int JournalId { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
}

[Table("MemberRequestReviewer")]
public class BecomeRequestDto
{
    [Key]
    [Column("Id")]
    public long RequestId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Designation { get; set; }
    public string? Country { get; set; }
    public string? BioData { get; set; }
    public string? Files { get; set; }
    public int? Type { get; set; }
    public int? JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public DateTime? CreatedDate { get; set; }
}
