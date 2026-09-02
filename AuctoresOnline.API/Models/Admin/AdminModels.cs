using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctoresOnline.API.Models.Admin;

[Table("Admin")]
public class AdminDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Mobile { get; set; } = "";
    public string Username { get; set; } = "";
    [JsonIgnore]
    public string? Password { get; set; }
    public short RoleType { get; set; }
    [JsonIgnore]
    public DateTime? CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedDate { get; set; }
}

public record LoginRequest(string Username, string Password);

public record LoginResponse(string Token, AdminDto Admin);

public record UpdateProfileRequest(string Name, string Email, string Mobile);

public record ChangePasswordRequest(string CurrentPassword, string NewPassword, string ConfirmPassword);

public record DashboardCountsDto(
    int TotalContacts, int TotalTestimonials, int TotalSubscribers,
    int TotalBanners, int TotalCollaborators, int TotalJournals,
    int TotalManuscripts, int TotalAuthors, int TotalEditors,
    int TotalEditorProfiles, int TotalMemberedIn, int TotalReviewers,
    int TotalArticlesInPress, int TotalCurrentIssues, int TotalArchives);
