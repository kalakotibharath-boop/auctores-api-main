using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctoresOnline.API.Models.Author;

[Table("Authors")]
public class AuthorDto
{
    [Key]
    public long AuthorId { get; set; }
    public string AuthorName { get; set; } = "";
    public string? AuthorEmail { get; set; }
    public string? AuthorPhone { get; set; }
    public string? AuthorCountry { get; set; }
    public string? AuthorDesignation { get; set; }
    public string? AuthorDescription { get; set; }
    public string? AuthorImage { get; set; }
    public string? AuthorAddress { get; set; }
    public byte AuthorStatus { get; set; }
    [JsonIgnore]
    public string? AuthorPassword { get; set; }
    public DateTime AuthorCreatedDate { get; set; }
    public DateTime? AuthorUpdatedDate { get; set; }
}

public class CreateAuthorRequest
{
    public string AuthorName { get; set; } = "";
    public string? AuthorEmail { get; set; }
    public string? AuthorPhone { get; set; }
    public string? AuthorCountry { get; set; }
    public string? AuthorDesignation { get; set; }
    public string? AuthorDescription { get; set; }
    public string? AuthorAddress { get; set; }
    public string? AuthorPassword { get; set; }
}
