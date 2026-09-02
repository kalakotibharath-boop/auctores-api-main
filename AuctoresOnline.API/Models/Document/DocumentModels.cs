using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Models.Document;

[Table("Documents")]
public class DocumentDto
{
    [Key]
    public long DocId { get; set; }
    public string? Title { get; set; }
    public string? PageSlug { get; set; }
    public string? DocData { get; set; }
    public long? JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public string? SeoKeywords { get; set; }
    public string? Type { get; set; }
    public int Status { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public class CreateDocumentRequest
{
    public string? Title { get; set; }
    public string? PageSlug { get; set; }
    public string? DocData { get; set; }
    public long? JournalId { get; set; }
    public string? SeoKeywords { get; set; }
    public string? Type { get; set; }
}
