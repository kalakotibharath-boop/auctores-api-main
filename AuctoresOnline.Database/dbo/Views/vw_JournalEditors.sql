CREATE VIEW [dbo].[vw_JournalEditors]
AS
SELECT
    jeb.[EditorialBoardId],
    jeb.[JournalId],
    jeb.[EditorId],
    jeb.[EditorRole],
    e.[EditorName],
    e.[EditorAddress],
    e.[EditorDesignation],
    e.[Qualification],
    e.[ProfileImage],
    e.[EditorEmail],
    e.[EditorOrcid],
    e.[EditorGoogleScholar],
    e.[EditorWebsite],
    e.[EditorPhone],
    j.[JournalName],
    j.[JournalSeoName],
    j.[JournalStatus],
    e.[EditorBiography]  AS [Biography],
    e.[EditorResearch]   AS [Research]
FROM [dbo].[JournalEditorialBoard] jeb
LEFT JOIN [dbo].[Editors] e ON jeb.[EditorId] = e.[EditorId]
LEFT JOIN [dbo].[Journals] j ON jeb.[JournalId] = j.[JournalId]
WHERE jeb.[EditorRole] <> N'Editor Profiles' OR jeb.[EditorRole] IS NULL;
GO
