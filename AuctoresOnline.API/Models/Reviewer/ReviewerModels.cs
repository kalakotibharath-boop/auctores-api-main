using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctoresOnline.API.Models.Reviewer;

[Table("Reviewers")]
public class ReviewerDto
{
    [Key]
    public long ReviewerId { get; set; }
    public string ReviewerName { get; set; } = "";
    public string? ReviewerEmail { get; set; }
    public string? ReviewerPhone { get; set; }
    public string? ReviewerCountry { get; set; }
    public string? ReviewerDesignation { get; set; }
    public string? ReviewerImage { get; set; }
    public string? ReviewerAddress { get; set; }
    public string? ReviewerDescription { get; set; }
    public byte ReviewerStatus { get; set; }
    [JsonIgnore]
    public string? ReviewerPassword { get; set; }
    public DateTime ReviewerCreatedDate { get; set; }
    public DateTime? ReviewerUpdatedDate { get; set; }
}

public class CreateReviewerRequest
{
    public string ReviewerName { get; set; } = "";
    public string? ReviewerEmail { get; set; }
    public string? ReviewerPhone { get; set; }
    public string? ReviewerCountry { get; set; }
    public string? ReviewerDesignation { get; set; }
    public string? ReviewerAddress { get; set; }
    public string? ReviewerDescription { get; set; }
    public string? ReviewerPassword { get; set; }
}
