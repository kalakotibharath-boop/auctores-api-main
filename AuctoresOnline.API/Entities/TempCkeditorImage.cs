using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Entities;

[Table("TempCkeditorImages")]
public class TempCkeditorImage
{
    [Key]
    public long Id { get; set; }
    public long ArticleId { get; set; }
    public string DataOf { get; set; } = "";
    public string FileName { get; set; } = "";
    public string FileSaved { get; set; } = "no";
    public DateTime? CreatedDate { get; set; }
}
