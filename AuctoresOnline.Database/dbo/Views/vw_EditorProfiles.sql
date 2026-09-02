CREATE VIEW [dbo].[vw_EditorProfiles]
AS
SELECT
    jeb.[EditorialBoardId],
    jeb.[JournalId],
    jeb.[EditorRole],
    ep.[EditorId],
    ep.[EditorName],
    ep.[EditorDesignation],
    ep.[EditorImage],
    ep.[EditorDescription],
    ep.[EditorStatus],
    j.[JournalName],
    j.[JournalSeoName],
    j.[JournalStatus]
FROM [dbo].[JournalEditorialBoard] jeb
LEFT JOIN [dbo].[EditorProfiles] ep ON jeb.[EditorId] = ep.[EditorId]
LEFT JOIN [dbo].[Journals] j ON jeb.[JournalId] = j.[JournalId]
WHERE jeb.[EditorRole] = N'Editor Profiles';
GO
