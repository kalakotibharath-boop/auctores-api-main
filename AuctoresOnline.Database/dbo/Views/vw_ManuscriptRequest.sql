CREATE VIEW [dbo].[vw_ManuscriptRequest]
AS
SELECT
    mr.[RequestId]                                  AS [ManuscriptId],
    CONCAT(mr.[FirstName], N' ', mr.[LastName])     AS [Name],
    mr.[Email],
    mr.[JournalId],
    mr.[Phone],
    mr.[Country],
    mr.[Orcid],
    mr.[Address],
    mr.[ManuscriptTitle]                            AS [Title],
    mr.[ArticleType],
    mr.[Abstract],
    mr.[Keywords],
    mr.[CoverLetter],
    mr.[Files],
    j.[JournalName],
    j.[JournalSeoName],
    mr.[Status],
    mr.[CreatedDate],
    mr.[UpdatedDate]
FROM [dbo].[ManuscriptRequest] mr
LEFT JOIN [dbo].[Journals] j ON mr.[JournalId] = j.[JournalId]
WHERE mr.[JournalId] IS NOT NULL AND mr.[JournalId] > 0;
GO
