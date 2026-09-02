using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctoresOnline.API.Models.Editor;

[Table("Editors")]
public class EditorDto
{
    [Key]
    public int EditorId { get; set; }
    public string EditorName { get; set; } = "";
    public string? EditorDesignation { get; set; }
    public string? Qualification { get; set; }
    public string? ProfileImage { get; set; }
    public string? EditorEmail { get; set; }
    public string? EditorPhone { get; set; }
    public string? EditorAddress { get; set; }
    public string? EditorBiography { get; set; }
    public string? EditorResearch { get; set; }
    public byte EditorStatus { get; set; }
    [JsonIgnore]
    public string? EditorPassword { get; set; }
    public string? EditorOrcid { get; set; }
    public string? EditorGoogleScholar { get; set; }
    public string? EditorWebsite { get; set; }
    public DateTime EditorCreatedDate { get; set; }
    public DateTime? EditorUpdatedDate { get; set; }
}

public class CreateEditorRequest
{
    public string EditorName { get; set; } = "";
    public string? EditorDesignation { get; set; }
    public string? Qualification { get; set; }
    public string? EditorEmail { get; set; }
    public string? EditorPhone { get; set; }
    public string? EditorAddress { get; set; }
    public string? EditorBiography { get; set; }
    public string? EditorResearch { get; set; }
    public string? EditorPassword { get; set; }
    public string? EditorOrcid { get; set; }
    public string? EditorGoogleScholar { get; set; }
    public string? EditorWebsite { get; set; }
}
