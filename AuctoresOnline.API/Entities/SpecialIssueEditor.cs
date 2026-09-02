using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Entities;

[Table("SpecialIssuesEditors")]
public class SpecialIssueEditor
{
    [Key]
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long EditorId { get; set; }
}
