CREATE VIEW [dbo].[vw_SpecialIssues]
AS
SELECT
    s.[IssueId],
    s.[Title],
    s.[JournalId],
    s.[Slug],
    s.[DeadlineDate],
    s.[ViewCount],
    s.[IssnNumber],
    s.[CitiesScore],
    s.[ImpactFactor],
    s.[FileLink],
    s.[IssueData],
    s.[Keywords],
    s.[Benefits],
    j.[JournalName],
    j.[JournalSeoName],
    s.[CreatedDate],
    s.[UpdatedDate],
    s.[Status]
FROM [dbo].[SpecialIssues] s
LEFT JOIN [dbo].[Journals] j ON s.[JournalId] = j.[JournalId];
GO
