CREATE VIEW [dbo].[vw_ArticleDetails]
AS
SELECT
    a.[ArticleId],
    a.[ArticleViews],
    a.[ArticleDownloads],
    a.[ArticleFor],
    a.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[IssnNumber],
    a.[VolumeNo],
    a.[IssueNo],
    a.[ArticleType],
    CASE a.[ArticleType]
        WHEN 0 THEN 'Article In Press'
        WHEN 1 THEN 'Current Issue'
        WHEN 2 THEN 'Archive'
        ELSE 'Unknown'
    END                         AS [ArticleTypeLabel],
    a.[ArticleName],
    a.[ArticleSeoName],
    a.[ArticleDoi],
    a.[CorrespondingAuthor],
    a.[ReceivedDate],
    a.[AcceptedDate],
    a.[PublishedDate],
    a.[Citation],
    a.[Copyright],
    a.[ArticleInformation],
    a.[ArticleAbstract],
    a.[DisplayArticleInformation],
    a.[DisplayArticleAbstract],
    a.[AddressDesc],
    a.[AbstractKeywords],
    a.[ArticlePdf],
    a.[ArticleStatus],
    a.[ArticleCreatedDate],
    a.[ArticleUpdatedDate]
FROM
    [dbo].[Articles] a
    INNER JOIN [dbo].[Journals] j ON a.[JournalId] = j.[JournalId];
GO
