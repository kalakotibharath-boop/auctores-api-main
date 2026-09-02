CREATE VIEW [dbo].[vw_CurrentIssueArticles]
AS
SELECT
    a.[ArticleId],
    a.[ArticleViews],
    a.[ArticleDownloads],
    a.[ArticleFor],
    a.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    a.[VolumeNo],
    a.[IssueNo],
    a.[ArticleName],
    a.[ArticleSeoName],
    a.[ArticleDoi],
    a.[CorrespondingAuthor],
    a.[ReceivedDate],
    a.[AcceptedDate],
    a.[PublishedDate],
    a.[Citation],
    a.[Copyright],
    a.[DisplayArticleInformation],
    a.[DisplayArticleAbstract],
    a.[AddressDesc],
    a.[AbstractKeywords],
    a.[ArticlePdf],
    a.[ArticleCreatedDate],
    a.[ArticleUpdatedDate]
FROM
    [dbo].[Articles] a
    INNER JOIN [dbo].[Journals] j ON a.[JournalId] = j.[JournalId]
WHERE
    a.[ArticleType]    = 1      -- Current Issue
    AND a.[ArticleStatus]  = 1
    AND j.[JournalStatus] = 1;
GO
