using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctoresOnline.API.Models.Journal;

[Table("Journals")]
public class JournalDto
{
    [Key]
    public int JournalId { get; set; }
    public string JournalName { get; set; } = "";
    public string? JournalDoi { get; set; }
    public string? JournalAcceptanceRate { get; set; }
    public string? JournalDecision { get; set; }
    public string? JournalAcceptancePublic { get; set; }
    public string? JournalCitescore1 { get; set; }
    public string? JournalImpact { get; set; }
    public string? JournalApc { get; set; }
    public string? JournalSnip { get; set; }
    public string? JournalSjr { get; set; }
    public string? JournalCitescore { get; set; }
    public int Volume { get; set; }
    public int Issue { get; set; }
    public int Year { get; set; }
    public string? Email { get; set; }
    public string? Scholar { get; set; }
    public string? IndexedArticle { get; set; }
    public string? IndexingImage { get; set; }
    public string? IndexingUrl { get; set; }
    public string? ImpactFactor { get; set; }
    public string? CrossrefUrl { get; set; }
    public string? CrossrefImage { get; set; }
    public string? IssnNumber { get; set; }
    public string? YoutubeLink { get; set; }
    public string? JournalSeoName { get; set; }
    public string? CallForPapers { get; set; }
    public string? AimsAndScope { get; set; }
    public long ChiefEditor { get; set; }
    public string? JournalImage { get; set; }
    public bool IsExternalJournal { get; set; }
    public int JournalStatus { get; set; }
    public DateTime JournalCreatedDate { get; set; }
    public DateTime? JournalUpdatedDate { get; set; }
}

public class JournalDetailDto : JournalDto
{
    [NotMapped]
    public List<JournalSectionDto> Sections { get; set; } = [];
    [NotMapped]
    public List<JournalEditorDto> Editors { get; set; } = [];
    [NotMapped]
    public List<EditorProfileBoardDto> EditorProfiles { get; set; } = [];
}

[Table("JournalSections")]
public class JournalSectionDto
{
    [Key]
    public int SectionId { get; set; }
    public int JournalId { get; set; }
    public string SectionName { get; set; } = "";
}

public class JournalEditorDto
{
    public long EditorialBoardId { get; set; }
    public long EditorId { get; set; }
    public string EditorName { get; set; } = "";
    public string? EditorDesignation { get; set; }
    public string? ProfileImage { get; set; }
    public string EditorRole { get; set; } = "";
}

public class EditorProfileBoardDto
{
    public long EditorialBoardId { get; set; }
    public long EditorId { get; set; }
    public string EditorName { get; set; } = "";
    public string? EditorImage { get; set; }
    public string? EditorDesignation { get; set; }
}

public class CreateJournalRequest
{
    public string JournalName { get; set; } = "";
    public string? JournalDoi { get; set; }
    public string? JournalAcceptanceRate { get; set; }
    public string? JournalDecision { get; set; }
    public string? JournalAcceptancePublic { get; set; }
    public string? JournalCitescore1 { get; set; }
    public string? JournalImpact { get; set; }
    public string? JournalApc { get; set; }
    public string? JournalSnip { get; set; }
    public string? JournalSjr { get; set; }
    public string? JournalCitescore { get; set; }
    public string? Volume { get; set; }
    public string? Issue { get; set; }
    public string? Year { get; set; }
    public string? Email { get; set; }
    public string? Scholar { get; set; }
    public string? IndexedArticle { get; set; }
    public string? IndexingUrl { get; set; }
    public string? ImpactFactor { get; set; }
    public string? CrossrefUrl { get; set; }
    public string? IssnNumber { get; set; }
    public string? YoutubeLink { get; set; }
    public string? JournalSeoName { get; set; }
    public string? CallForPapers { get; set; }
    public string? AimsAndScope { get; set; }
    public long? ChiefEditor { get; set; }
    public bool IsExternalJournal { get; set; }
    public List<long> ManuscriptEditorIds { get; set; } = [];
    public List<long> AssistantEditorIds { get; set; } = [];
    public List<long> ConsultingEditorIds { get; set; } = [];
    public List<long> AssociateEditorIds { get; set; } = [];
    public List<long> EditorProfileIds { get; set; } = [];
    public List<string> Sections { get; set; } = [];
}
