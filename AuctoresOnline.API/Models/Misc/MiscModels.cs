using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Models.Misc;

[Table("Banners")]
public class BannerDto
{
    [Key]
    public long BannerId { get; set; }
    public string? BannerHeading { get; set; }
    public string? BannerDescription { get; set; }
    public string? BannerImage { get; set; }
    public byte BannerStatus { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public record CreateBannerRequest(string? BannerHeading, string? BannerDescription);

[Table("Collaborators")]
public class CollaboratorDto
{
    [Key]
    public int CollaboratorId { get; set; }
    public string? CollaboratorName { get; set; }
    public string? CollaboratorUrl { get; set; }
    public string? CollaboratorImage { get; set; }
    public byte CollaboratorStatus { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public record CreateCollaboratorRequest(string? CollaboratorName, string? CollaboratorUrl);

[Table("Testimonials")]
public class TestimonialDto
{
    [Key]
    public int TestimonialId { get; set; }
    public string? TestimonialName { get; set; }
    public string? TestimonialImage { get; set; }
    public string? TestimonialDescription { get; set; }
    public int TestimonialStatus { get; set; }
    public DateTime TestimonialCreatedDate { get; set; }
    public DateTime? TestimonialUpdatedDate { get; set; }
}

public record CreateTestimonialRequest(string? TestimonialName, string? TestimonialDescription);

[Table("Contacts")]
public class ContactDto
{
    [Key]
    public long ContactId { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactSubject { get; set; }
    public string? ContactMessage { get; set; }
    public string? ContactStatus { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public record UpdateContactStatusRequest(string Status);

[Table("SubscriptionList")]
public class SubscriberDto
{
    [Key]
    public long SubscriptionId { get; set; }
    public string? EmailId { get; set; }
    public string? SubscribedFor { get; set; }
    public long? SubscribedForId { get; set; }
    [NotMapped]
    public string? JournalName { get; set; }
    public byte SubscriptionStatus { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
