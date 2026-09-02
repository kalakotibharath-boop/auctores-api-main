CREATE VIEW [dbo].[vw_SpecialIssue]
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
    s.[IssueDataDisplay],
    s.[Keywords],
    s.[Benefits],
    s.[CreatedDate],
    s.[UpdatedDate],
    s.[Status],
    j.[JournalName],
    j.[JournalSeoName],
    (
        SELECT si2.[EditorId] AS editor_id, e2.[EditorName] AS editor_name
        FROM [dbo].[SpecialIssuesEditors] si2
        INNER JOIN [dbo].[Editors] e2 ON si2.[EditorId] = e2.[EditorId]
        WHERE si2.[IssueId] = s.[IssueId]
        FOR JSON PATH
    ) AS [EditorsData]
FROM [dbo].[SpecialIssues] s
LEFT JOIN [dbo].[Journals] j ON s.[JournalId] = j.[JournalId];
GO
