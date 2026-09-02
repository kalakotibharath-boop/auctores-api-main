CREATE VIEW [dbo].[vw_Documents]
AS
SELECT
    d.[DocId],
    d.[Title],
    d.[PageSlug],
    d.[DocData],
    d.[JournalId],
    d.[Type],
    d.[SeoKeywords],
    d.[Status],
    d.[CreatedBy],
    d.[CreatedDate],
    d.[UpdatedDate],
    j.[JournalName],
    j.[JournalSeoName]
FROM [dbo].[Documents] d
LEFT JOIN [dbo].[Journals] j ON d.[JournalId] = j.[JournalId];
GO
