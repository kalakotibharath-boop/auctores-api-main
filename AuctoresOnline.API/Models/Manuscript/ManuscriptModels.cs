using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctoresOnline.API.Models.Manuscript;

[Table("ManuscriptRequest")]
public class ManuscriptRequestDto
{
    [Key]
    [JsonIgnore]
    public long RequestId { get; set; }
    [NotMapped]
    public long ManuscriptId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [NotMapped]
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public string? Orcid { get; set; }
    public string? Address { get; set; }
    public long? JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public string? ManuscriptTitle { get; set; }
    public string? ArticleType { get; set; }
    public string? Abstract { get; set; }
    public string? Keywords { get; set; }
    public string? BioData { get; set; }
    public string? Files { get; set; }
    public string? CoverLetter { get; set; }
    public byte Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public record ChangeManuscriptStatusRequest(byte Status);
