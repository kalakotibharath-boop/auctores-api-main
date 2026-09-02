using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctoresOnline.API.Models.EditorProfile;

[Table("EditorProfiles")]
public class EditorProfileDto
{
    [Key]
    public long EditorId { get; set; }
    public string EditorName { get; set; } = "";
    public string? EditorEmail { get; set; }
    public string? EditorPhone { get; set; }
    public string? EditorDesignation { get; set; }
    public string? EditorImage { get; set; }
    public string? EditorDescription { get; set; }
    public byte EditorStatus { get; set; }
    [JsonIgnore]
    public string? EditorPassword { get; set; }
    public DateTime EditorCreatedDate { get; set; }
    public DateTime? EditorUpdatedDate { get; set; }
}

public class CreateEditorProfileRequest
{
    public string EditorName { get; set; } = "";
    public string? EditorEmail { get; set; }
    public string? EditorPhone { get; set; }
    public string? EditorDesignation { get; set; }
    public string? EditorDescription { get; set; }
    public string? EditorPassword { get; set; }
}
