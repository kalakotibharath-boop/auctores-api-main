using Microsoft.EntityFrameworkCore;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Models.Common;
using AuctoresOnline.API.Models.User;
using AuctoresOnline.API.Services.Interfaces.User;

namespace AuctoresOnline.API.Services.Implementations.User;

public class UserJournalService(ApplicationDbContext context) : IUserJournalService
{
    // ── Journal alphabetical list with testimonials ────────────────────────────

    public async Task<ServiceResult<JournalListingDto>> GetJournalListAsync()
    {
        var allJournals = await context.Journals
            .Where(j => j.JournalStatus == 1)
            .OrderBy(j => j.JournalName)
            .Select(j => new JournalListItemDto
            {
                JournalName = j.JournalName,
                JournalSeoName = j.JournalSeoName,
                IsExternalJournal = j.IsExternalJournal
            })
            .ToListAsync();

        var startingLetters = allJournals
            .Select(j => j.JournalName[..1].ToUpper())
            .Distinct()
            .OrderBy(l => l)
            .ToList();

        var grouped = new Dictionary<string, List<JournalListItemDto>>();
        foreach (var journal in allJournals)
        {
            var letter = journal.JournalName[..1].ToUpper();
            if (!grouped.ContainsKey(letter))
                grouped[letter] = [];
            grouped[letter].Add(journal);
        }

        var testimonials = await context.Testimonials
            .Where(t => t.TestimonialStatus == 1)
            .OrderBy(t => Guid.NewGuid())
            .Take(6)
            .Select(t => new TestimonialPublicDto
            {
                TestimonialId = t.TestimonialId,
                TestimonialName = t.TestimonialName,
                TestimonialImage = t.TestimonialImage,
                TestimonialDescription = t.TestimonialDescription
            })
            .ToListAsync();

        return ServiceResult<JournalListingDto>.Ok(new JournalListingDto
        {
            StartingLetters = startingLetters,
            Journals = grouped,
            Testimonials = testimonials
        });
    }

    // ── Journal homepage / overview ───────────────────────────────────────────

    public async Task<ServiceResult<JournalPublicDetailDto>> GetJournalDetailAsync(string seoName)
    {
        var journal = await context.Journals
            .Where(j => j.JournalStatus == 1 && j.JournalSeoName == seoName)
            .FirstOrDefaultAsync();

        if (journal is null)
            return ServiceResult<JournalPublicDetailDto>.NotFound("Journal not found.");

        var sections = await context.JournalSections
            .Where(s => s.JournalId == journal.JournalId)
            .OrderBy(s => s.SectionName)
            .Take(15)
            .Select(s => new JournalSectionPublicDto { SectionId = s.SectionId, SectionName = s.SectionName })
            .ToListAsync();

        var recentArticles = await GetArticleListByJournalAsync(journal.JournalId, descByDate: true, take: 6);

        var pubmedIndex = await context.PubmedIndex
            .Where(p => p.JournalId == journal.JournalId)
            .OrderByDescending(p => p.PubmedIndexId)
            .Take(4)
            .Select(p => new PubmedIndexPublicDto
            {
                PubmedIndexId = p.PubmedIndexId,
                IndexText = p.IndexText,
                PubmedUrl = p.PubmedUrl,
                PmcUrl = p.PmcUrl
            })
            .ToListAsync();

        var members = await context.MemberedIn
            .Where(m => m.JournalId == journal.JournalId && m.Status == 1)
            .OrderByDescending(m => m.Id)
            .Take(30)
            .Select(m => new MemberedInPublicDto { Id = m.Id, Name = m.Name, Image = m.Image, Url = m.Url })
            .ToListAsync();

        return ServiceResult<JournalPublicDetailDto>.Ok(new JournalPublicDetailDto
        {
            JournalId = journal.JournalId,
            JournalName = journal.JournalName,
            JournalSeoName = journal.JournalSeoName,
            JournalDoi = journal.JournalDoi,
            IssnNumber = journal.IssnNumber,
            JournalApc = journal.JournalApc,
            JournalImpact = journal.JournalImpact,
            ImpactFactor = journal.ImpactFactor,
            CallForPapers = journal.CallForPapers,
            AimsAndScope = journal.AimsAndScope,
            JournalImage = journal.JournalImage,
            Volume = journal.Volume,
            Issue = journal.Issue,
            Year = journal.Year,
            Email = journal.Email,
            YoutubeLink = journal.YoutubeLink,
            JournalAcceptanceRate = journal.JournalAcceptanceRate,
            JournalCitescore = journal.JournalCitescore,
            CrossrefUrl = journal.CrossrefUrl,
            IndexingUrl = journal.IndexingUrl,
            IsExternalJournal = journal.IsExternalJournal,
            Sections = sections,
            RecentArticles = recentArticles,
            PubmedIndex = pubmedIndex,
            Members = members
        });
    }

    // ── Editorial board ───────────────────────────────────────────────────────

    public async Task<ServiceResult<EditorialBoardDto>> GetEditorialBoardAsync(string seoName)
    {
        var journal = await context.Journals
            .Where(j => j.JournalStatus == 1 && j.JournalSeoName == seoName)
            .FirstOrDefaultAsync();

        if (journal is null)
            return ServiceResult<EditorialBoardDto>.NotFound("Journal not found.");

        var members = await context.JournalEditorialBoards
            .Where(b => b.JournalId == journal.JournalId && b.EditorRole != "Editor Profiles")
            .Join(context.Editors, b => b.EditorId, e => e.EditorId,
                (b, e) => new EditorBoardMemberDto
                {
                    EditorId = e.EditorId,
                    EditorName = e.EditorName,
                    EditorDesignation = e.EditorDesignation,
                    ProfileImage = e.ProfileImage,
                    EditorRole = b.EditorRole
                })
            .ToListAsync();

        var roles = members.Select(m => m.EditorRole).Distinct().ToList();

        return ServiceResult<EditorialBoardDto>.Ok(new EditorialBoardDto
        {
            JournalId = journal.JournalId,
            JournalName = journal.JournalName,
            JournalImage = journal.JournalImage,
            Roles = roles,
            Members = members
        });
    }

    // ── Editor profiles ───────────────────────────────────────────────────────

    public async Task<ServiceResult<EditorProfilesPublicDto>> GetEditorProfilesAsync(string seoName)
    {
        var journal = await context.Journals
            .Where(j => j.JournalStatus == 1 && j.JournalSeoName == seoName)
            .FirstOrDefaultAsync();

        if (journal is null)
            return ServiceResult<EditorProfilesPublicDto>.NotFound("Journal not found.");

        var profiles = await context.JournalEditorialBoards
            .Where(b => b.JournalId == journal.JournalId && b.EditorRole == "Editor Profiles")
            .Join(context.EditorProfiles, b => b.EditorId, ep => ep.EditorId,
                (b, ep) => new EditorProfileMemberDto
                {
                    EditorId = ep.EditorId,
                    EditorName = ep.EditorName,
                    EditorDesignation = ep.EditorDesignation,
                    EditorImage = ep.EditorImage,
                    EditorDescription = ep.EditorDescription
                })
            .ToListAsync();

        return ServiceResult<EditorProfilesPublicDto>.Ok(new EditorProfilesPublicDto
        {
            JournalId = journal.JournalId,
            JournalName = journal.JournalName,
            JournalImage = journal.JournalImage,
            Members = profiles
        });
    }

    // ── Articles in press (article_type = 0) ──────────────────────────────────

    public async Task<ServiceResult<IEnumerable<ArticleListItemDto>>> GetArticlesInPressAsync(string seoName)
    {
        var journal = await FindActiveJournalAsync(seoName);
        if (journal is null)
            return ServiceResult<IEnumerable<ArticleListItemDto>>.NotFound("Journal not found.");

        var articles = await GetArticleListByJournalAsync(journal.JournalId, articleType: 0, descByDate: true);
        return ServiceResult<IEnumerable<ArticleListItemDto>>.Ok(articles);
    }

    // ── Current issues (article_type = 1) ────────────────────────────────────

    public async Task<ServiceResult<IEnumerable<ArticleListItemDto>>> GetCurrentIssuesAsync(string seoName)
    {
        var journal = await FindActiveJournalAsync(seoName);
        if (journal is null)
            return ServiceResult<IEnumerable<ArticleListItemDto>>.NotFound("Journal not found.");

        var articles = await GetArticleListByJournalAsync(journal.JournalId, articleType: 1, descByDate: true, activeOnly: true);
        return ServiceResult<IEnumerable<ArticleListItemDto>>.Ok(articles);
    }

    // ── Archive structure (volumes + issues) ──────────────────────────────────

    public async Task<ServiceResult<ArchiveDataDto>> GetArchivesAsync(string seoName)
    {
        var journal = await FindActiveJournalAsync(seoName);
        if (journal is null)
            return ServiceResult<ArchiveDataDto>.NotFound("Journal not found.");

        var archiveData = await context.Articles
            .Where(a => a.JournalId == journal.JournalId && a.ArticleType == 2)
            .Where(a => a.PublishedDate.HasValue)
            .OrderByDescending(a => a.VolumeNo)
            .ThenByDescending(a => a.IssueNo)
            .ThenBy(a => a.PublishedDate)
            .Select(a => new
            {
                a.VolumeNo,
                a.IssueNo,
                PublishedYear = a.PublishedDate!.Value.Year,
                PublishedMonth = a.PublishedDate!.Value.Month
            })
            .ToListAsync();

        var seenVolumes = new HashSet<int>();
        var volumes = new List<ArchiveVolumeDto>();
        var seenIssues = new HashSet<string>();
        var issues = new List<ArchiveIssueDto>();

        foreach (var row in archiveData)
        {
            if (seenVolumes.Add(row.VolumeNo))
                volumes.Add(new ArchiveVolumeDto { VolumeNo = row.VolumeNo, PublishedYear = row.PublishedYear });

            var issueKey = $"{row.VolumeNo}-{row.IssueNo}";
            if (seenIssues.Add(issueKey))
                issues.Add(new ArchiveIssueDto
                {
                    VolumeNo = row.VolumeNo,
                    IssueNo = row.IssueNo,
                    PublishedYear = row.PublishedYear,
                    PublishedMonth = new DateTime(row.PublishedYear, row.PublishedMonth, 1).ToString("MMMM")
                });
        }

        return ServiceResult<ArchiveDataDto>.Ok(new ArchiveDataDto { Volumes = volumes, Issues = issues });
    }

    // ── Articles for a specific archive volume/issue ──────────────────────────

    public async Task<ServiceResult<IEnumerable<ArticleListItemDto>>> GetArchiveArticlesAsync(
        string seoName, int volumeNo, int issueNo)
    {
        var journal = await FindActiveJournalAsync(seoName);
        if (journal is null)
            return ServiceResult<IEnumerable<ArticleListItemDto>>.NotFound("Journal not found.");

        var articles = await context.Articles
            .Where(a => a.JournalId == journal.JournalId
                     && a.ArticleType == 2
                     && a.ArticleStatus == 1
                     && a.VolumeNo == volumeNo
                     && a.IssueNo == issueNo)
            .OrderByDescending(a => a.PublishedDate)
            .Select(a => new ArticleListItemDto
            {
                ArticleId = a.ArticleId,
                ArticleName = a.ArticleName,
                ArticleSeoName = a.ArticleSeoName,
                ArticlePdf = a.ArticlePdf,
                PublishedDate = a.PublishedDate,
                ArticleFor = a.ArticleFor,
                VolumeNo = a.VolumeNo,
                IssueNo = a.IssueNo
            })
            .ToListAsync();

        await AttachAuthorNamesAsync(articles);
        return ServiceResult<IEnumerable<ArticleListItemDto>>.Ok(articles);
    }

    // ── PubMed index ──────────────────────────────────────────────────────────

    public async Task<ServiceResult<IEnumerable<PubmedIndexPublicDto>>> GetPubmedIndexAsync(string seoName)
    {
        var journal = await FindActiveJournalAsync(seoName);
        if (journal is null)
            return ServiceResult<IEnumerable<PubmedIndexPublicDto>>.NotFound("Journal not found.");

        var data = await context.PubmedIndex
            .Where(p => p.JournalId == journal.JournalId)
            .OrderByDescending(p => p.PubmedIndexId)
            .Select(p => new PubmedIndexPublicDto
            {
                PubmedIndexId = p.PubmedIndexId,
                IndexText = p.IndexText,
                PubmedUrl = p.PubmedUrl,
                PmcUrl = p.PmcUrl
            })
            .ToListAsync();

        return ServiceResult<IEnumerable<PubmedIndexPublicDto>>.Ok(data);
    }

    // ── Abstracting & indexing ────────────────────────────────────────────────

    public async Task<ServiceResult<IEnumerable<AbstractingIndexingPublicDto>>> GetAbstractIndexingAsync(string seoName)
    {
        var journal = await FindActiveJournalAsync(seoName);
        if (journal is null)
            return ServiceResult<IEnumerable<AbstractingIndexingPublicDto>>.NotFound("Journal not found.");

        var data = await context.AbstractingIndexing
            .Where(ai => ai.JournalId == journal.JournalId && ai.Status == 1)
            .OrderBy(ai => ai.AbstractionTitle)
            .Select(ai => new AbstractingIndexingPublicDto
            {
                AbstractionId = ai.AbstractionId,
                AbstractionTitle = ai.AbstractionTitle,
                AbstractionUrl = ai.AbstractionUrl
            })
            .ToListAsync();

        return ServiceResult<IEnumerable<AbstractingIndexingPublicDto>>.Ok(data);
    }

    // ── Special issues for a journal ──────────────────────────────────────────

    public async Task<ServiceResult<IEnumerable<SpecialIssueListItemDto>>> GetSpecialIssuesAsync(string seoName)
    {
        var journal = await FindActiveJournalAsync(seoName);
        if (journal is null)
            return ServiceResult<IEnumerable<SpecialIssueListItemDto>>.NotFound("Journal not found.");

        var data = await context.SpecialIssues
            .Where(si => si.JournalId == journal.JournalId && si.Status == 1)
            .OrderByDescending(si => si.IssueId)
            .Select(si => new SpecialIssueListItemDto
            {
                IssueId = si.IssueId,
                Title = si.Title,
                Slug = si.Slug,
                DeadlineDate = si.DeadlineDate,
                Keywords = si.Keywords
            })
            .ToListAsync();

        return ServiceResult<IEnumerable<SpecialIssueListItemDto>>.Ok(data);
    }

    // ── About journal page ────────────────────────────────────────────────────

    public async Task<ServiceResult<JournalPublicDetailDto>> GetAboutJournalAsync(string seoName)
    {
        var journal = await context.Journals
            .Where(j => j.JournalStatus == 1 && j.JournalSeoName == seoName)
            .FirstOrDefaultAsync();

        if (journal is null)
            return ServiceResult<JournalPublicDetailDto>.NotFound("Journal not found.");

        var sections = await context.JournalSections
            .Where(s => s.JournalId == journal.JournalId)
            .OrderBy(s => s.SectionName)
            .Select(s => new JournalSectionPublicDto { SectionId = s.SectionId, SectionName = s.SectionName })
            .ToListAsync();

        var recentArticles = await context.Articles
            .Where(a => a.ArticleStatus == 1)
            .OrderByDescending(a => a.ArticleId)
            .Take(6)
            .Select(a => new ArticleListItemDto
            {
                ArticleId = a.ArticleId,
                ArticleName = a.ArticleName,
                ArticleSeoName = a.ArticleSeoName,
                ArticlePdf = a.ArticlePdf,
                PublishedDate = a.PublishedDate,
                ArticleFor = a.ArticleFor,
                VolumeNo = a.VolumeNo,
                IssueNo = a.IssueNo
            })
            .ToListAsync();

        return ServiceResult<JournalPublicDetailDto>.Ok(new JournalPublicDetailDto
        {
            JournalId = journal.JournalId,
            JournalName = journal.JournalName,
            JournalSeoName = journal.JournalSeoName,
            JournalDoi = journal.JournalDoi,
            IssnNumber = journal.IssnNumber,
            JournalApc = journal.JournalApc,
            JournalImpact = journal.JournalImpact,
            ImpactFactor = journal.ImpactFactor,
            CallForPapers = journal.CallForPapers,
            AimsAndScope = journal.AimsAndScope,
            JournalImage = journal.JournalImage,
            Volume = journal.Volume,
            Issue = journal.Issue,
            Year = journal.Year,
            Email = journal.Email,
            YoutubeLink = journal.YoutubeLink,
            IsExternalJournal = journal.IsExternalJournal,
            Sections = sections,
            RecentArticles = recentArticles
        });
    }

    // ── Journal dropdown options for manuscript submission form ────────────────

    public async Task<ServiceResult<IEnumerable<JournalSubmitOptionDto>>> GetJournalsForSelectAsync()
    {
        var journals = await context.Journals
            .Where(j => j.JournalStatus == 1)
            .OrderBy(j => j.JournalName)
            .Select(j => new JournalSubmitOptionDto
            {
                JournalId = j.JournalId,
                JournalName = j.JournalName,
                JournalSeoName = j.JournalSeoName
            })
            .ToListAsync();

        return ServiceResult<IEnumerable<JournalSubmitOptionDto>>.Ok(journals);
    }

    // ── Article processing charges ────────────────────────────────────────────

    public async Task<ServiceResult<IEnumerable<ApcPublicDto>>> GetAllArticleProcessingChargesAsync()
    {
        var data = await context.ArticleProcessingCharges
            .Join(context.Journals, apc => apc.JournalId, j => j.JournalId,
                (apc, j) => new ApcPublicDto
                {
                    JournalId = apc.JournalId,
                    JournalName = j.JournalName,
                    JournalSeoName = j.JournalSeoName,
                    ApcAmount = apc.ApcAmount
                })
            .OrderBy(x => x.JournalName)
            .ToListAsync();

        return ServiceResult<IEnumerable<ApcPublicDto>>.Ok(data);
    }

    // ── Private helpers ───────────────────────────────────────────────────────

    private sealed record JournalSummary(int JournalId, string JournalName, string? JournalImage, string? JournalSeoName);

    private async Task<JournalSummary?> FindActiveJournalAsync(string seoName)
        => await context.Journals
            .Where(j => j.JournalStatus == 1 && j.JournalSeoName == seoName)
            .Select(j => new JournalSummary(j.JournalId, j.JournalName, j.JournalImage, j.JournalSeoName))
            .FirstOrDefaultAsync();

    private async Task<List<ArticleListItemDto>> GetArticleListByJournalAsync(
        int journalId,
        byte? articleType = null,
        bool descByDate = false,
        bool activeOnly = false,
        int? take = null)
    {
        var query = context.Articles.Where(a => a.JournalId == journalId);

        if (activeOnly) query = query.Where(a => a.ArticleStatus == 1);
        if (articleType.HasValue) query = query.Where(a => a.ArticleType == articleType.Value);

        IQueryable<Models.Article.ArticleDto> ordered = descByDate
            ? query.OrderByDescending(a => a.PublishedDate)
            : query.OrderBy(a => a.PublishedDate);

        if (take.HasValue) ordered = ordered.Take(take.Value);

        var articles = await ordered
            .Select(a => new ArticleListItemDto
            {
                ArticleId = a.ArticleId,
                ArticleName = a.ArticleName,
                ArticleSeoName = a.ArticleSeoName,
                ArticlePdf = a.ArticlePdf,
                PublishedDate = a.PublishedDate,
                ArticleFor = a.ArticleFor,
                VolumeNo = a.VolumeNo,
                IssueNo = a.IssueNo
            })
            .ToListAsync();

        await AttachAuthorNamesAsync(articles);
        return articles;
    }

    // Loads all author names for the given articles in a single query and
    // groups them as a comma-separated string on each item.
    private async Task AttachAuthorNamesAsync(List<ArticleListItemDto> articles)
    {
        if (articles.Count == 0) return;

        var ids = articles.Select(a => a.ArticleId).ToList();

        var authorGroups = await context.ArticleAuthors
            .Where(au => ids.Contains(au.ArticleId))
            .OrderBy(au => au.ArticleId)
            .ThenBy(au => au.AuthorNo)
            .Select(au => new { au.ArticleId, au.AuthorName })
            .ToListAsync();

        var lookup = authorGroups
            .GroupBy(x => x.ArticleId)
            .ToDictionary(g => g.Key, g => string.Join(", ", g.Select(x => x.AuthorName)));

        foreach (var article in articles)
            article.AllAuthorNames = lookup.GetValueOrDefault(article.ArticleId);
    }
}
