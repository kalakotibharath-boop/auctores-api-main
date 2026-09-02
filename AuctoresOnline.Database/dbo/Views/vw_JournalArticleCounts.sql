CREATE VIEW [dbo].[vw_JournalArticleCounts]
AS
SELECT
    j.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[IssnNumber],
    j.[JournalStatus],
    COUNT(a.[ArticleId])    AS [TotalArticles],
    SUM(CASE WHEN a.[ArticleType] = 0 AND a.[ArticleStatus] = 1 THEN 1 ELSE 0 END) AS [ArticlesInPress],
    SUM(CASE WHEN a.[ArticleType] = 1 AND a.[ArticleStatus] = 1 THEN 1 ELSE 0 END) AS [CurrentIssueArticles],
    SUM(CASE WHEN a.[ArticleType] = 2 AND a.[ArticleStatus] = 1 THEN 1 ELSE 0 END) AS [ArchiveArticles],
    SUM(ISNULL(a.[ArticleViews],     0))                                            AS [TotalViews],
    SUM(ISNULL(a.[ArticleDownloads], 0))                                            AS [TotalDownloads]
FROM
    [dbo].[Journals] j
    LEFT JOIN [dbo].[Articles] a ON j.[JournalId] = a.[JournalId]
GROUP BY
    j.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[IssnNumber],
    j.[JournalStatus];
GO
