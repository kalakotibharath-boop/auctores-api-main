using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Controllers.User;

/// <summary>
/// All public journal-related pages: listing, detail, editorial board,
/// articles-in-press, current issues, archives, and more.
/// </summary>
[Route("api/user/journals")]
public class JournalsController(IUserJournalService journalService) : UserBaseController
{
    // ── Journal listing ───────────────────────────────────────────────────────

    /// <summary>
    /// Returns all active journals grouped alphabetically plus 6 random testimonials.
    /// Used on the /journals page.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetJournalList))]
    public async Task<IActionResult> GetJournalList()
        => ToResult(await journalService.GetJournalListAsync());

    /// <summary>
    /// Returns the list of active journals as a simple id/name dropdown for
    /// use in the submit-manuscript form.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetJournalsForSelect))]
    public async Task<IActionResult> GetJournalsForSelect()
        => ToResult(await journalService.GetJournalsForSelectAsync());

    /// <summary>
    /// Returns article processing charges for all journals.
    /// Used on the /article-processing-charges page.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetArticleProcessingCharges))]
    public async Task<IActionResult> GetArticleProcessingCharges()
        => ToResult(await journalService.GetAllArticleProcessingChargesAsync());

    // ── Single journal pages ──────────────────────────────────────────────────

    /// <summary>
    /// Returns full journal detail: info, sections, recent articles,
    /// PubMed index entries, and membered-in logos.
    /// Used on /journals/{seoName}.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetJournalDetail))]
    public async Task<IActionResult> GetJournalDetail(string seoName)
        => ToResult(await journalService.GetJournalDetailAsync(seoName));

    /// <summary>
    /// Returns the editorial board members grouped by role.
    /// Used on /journals/{seoName}/editorial-board.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetEditorialBoard))]
    public async Task<IActionResult> GetEditorialBoard(string seoName)
        => ToResult(await journalService.GetEditorialBoardAsync(seoName));

    /// <summary>
    /// Returns the editor profiles (a distinct group from editorial board).
    /// Used on /journals/{seoName}/editor-profiles.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetEditorProfiles))]
    public async Task<IActionResult> GetEditorProfiles(string seoName)
        => ToResult(await journalService.GetEditorProfilesAsync(seoName));

    /// <summary>
    /// Returns articles currently in press (article_type = 0) for the journal.
    /// Used on /journals/{seoName}/articles-in-press.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetArticlesInPress))]
    public async Task<IActionResult> GetArticlesInPress(string seoName)
        => ToResult(await journalService.GetArticlesInPressAsync(seoName));

    /// <summary>
    /// Returns current issue articles (article_type = 1, active only).
    /// Used on /journals/{seoName}/current-issues.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetCurrentIssues))]
    public async Task<IActionResult> GetCurrentIssues(string seoName)
        => ToResult(await journalService.GetCurrentIssuesAsync(seoName));

    /// <summary>
    /// Returns the archive structure: unique volumes and volume/issue combinations.
    /// Used on /journals/{seoName}/archives.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetArchives))]
    public async Task<IActionResult> GetArchives(string seoName)
        => ToResult(await journalService.GetArchivesAsync(seoName));

    /// <summary>
    /// Returns the articles in a specific archive volume and issue.
    /// Used on /journals/{seoName}/archives/volume-{v}/issue-{i}.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetArchiveArticles))]
    public async Task<IActionResult> GetArchiveArticles(string seoName, int volumeNo, int issueNo)
        => ToResult(await journalService.GetArchiveArticlesAsync(seoName, volumeNo, issueNo));

    /// <summary>
    /// Returns the PubMed indexing data for the journal.
    /// Used on /journals/{seoName}/pubmed-index.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetPubmedIndex))]
    public async Task<IActionResult> GetPubmedIndex(string seoName)
        => ToResult(await journalService.GetPubmedIndexAsync(seoName));

    /// <summary>
    /// Returns abstracting and indexing entries for the journal.
    /// Used on /journals/{seoName}/abstract-indexing.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetAbstractIndexing))]
    public async Task<IActionResult> GetAbstractIndexing(string seoName)
        => ToResult(await journalService.GetAbstractIndexingAsync(seoName));

    /// <summary>
    /// Returns active special issues for the journal.
    /// Used on /journals/{seoName}/special-issues.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetSpecialIssues))]
    public async Task<IActionResult> GetSpecialIssues(string seoName)
        => ToResult(await journalService.GetSpecialIssuesAsync(seoName));

    /// <summary>
    /// Returns the about-journal page data including sections and recent articles.
    /// Used on /journals/{seoName}/about-journal.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetAboutJournal))]
    public async Task<IActionResult> GetAboutJournal(string seoName)
        => ToResult(await journalService.GetAboutJournalAsync(seoName));
}
