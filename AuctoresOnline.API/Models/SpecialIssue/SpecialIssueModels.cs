using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Models.SpecialIssue;

[Table("SpecialIssues")]
public class SpecialIssueDto
{
    [Key]
    public long IssueId { get; set; }
    public string? Title { get; set; }
    public long JournalId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    [NotMapped]
    public string? JournalSeoName { get; set; }
    public string? Slug { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public int ViewCount { get; set; }
    public string? IssnNumber { get; set; }
    public string? CitiesScore { get; set; }
    public string? ImpactFactor { get; set; }
    public string? FileLink { get; set; }
    public string? IssueData { get; set; }
    public string? IssueDataDisplay { get; set; }
    public string? Keywords { get; set; }
    public string? Benefits { get; set; }
    public byte Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    [NotMapped]
    public List<long> SelectedEditorIds { get; set; } = [];
}

public class CreateSpecialIssueRequest
{
    public string? Title { get; set; }
    public long JournalId { get; set; }
    public string? Slug { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public string? IssnNumber { get; set; }
    public string? CitiesScore { get; set; }
    public string? ImpactFactor { get; set; }
    public string? IssueData { get; set; }
    public string? IssueDataDisplay { get; set; }
    public string? Keywords { get; set; }
    public string? Benefits { get; set; }
    public List<long> SelectedEditors { get; set; } = [];
}
