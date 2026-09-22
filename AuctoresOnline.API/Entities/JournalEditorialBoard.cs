using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Entities;

[Table("JournalEditorialBoard")]
public class JournalEditorialBoard
{
    [Key]
    public int EditorialBoardId { get; set; }
    public int JournalId { get; set; }
    public int EditorId { get; set; }
    public string EditorRole { get; set; } = "";
}
