CREATE VIEW [dbo].[vw_SpecialIssueEditors]
AS
SELECT
    sie.[IssueEditorId],
    sie.[IssueId],
    e.[EditorId],
    e.[EditorName],
    e.[EditorDesignation],
    e.[Qualification],
    e.[ProfileImage],
    e.[EditorEmail],
    e.[EditorPhone],
    e.[EditorAddress],
    e.[EditorBiography],
    e.[EditorResearch],
    e.[EditorStatus],
    e.[EditorPassword],
    e.[EditorOrcid],
    e.[EditorGoogleScholar],
    e.[EditorWebsite],
    e.[EditorCreatedDate],
    e.[EditorUpdatedDate]
FROM [dbo].[SpecialIssuesEditors] sie
LEFT JOIN [dbo].[Editors] e ON sie.[EditorId] = e.[EditorId];
GO
