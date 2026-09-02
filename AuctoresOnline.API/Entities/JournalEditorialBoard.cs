using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Entities;

[Table("JournalEditorialBoard")]
public class JournalEditorialBoard
{
    [Key]
    public long EditorialBoardId { get; set; }
    public int JournalId { get; set; }
    public long EditorId { get; set; }
    public string EditorRole { get; set; } = "";
}
