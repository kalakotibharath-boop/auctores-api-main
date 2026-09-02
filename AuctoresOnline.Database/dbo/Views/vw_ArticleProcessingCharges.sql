CREATE VIEW [dbo].[vw_ArticleProcessingCharges]
AS
SELECT
    apc.[ApcId],
    apc.[JournalId],
    apc.[ApcAmount],
    apc.[CreatedDate],
    apc.[UpdatedDate],
    j.[JournalName],
    j.[JournalSeoName]
FROM [dbo].[ArticleProcessingCharges] apc
LEFT JOIN [dbo].[Journals] j ON apc.[JournalId] = j.[JournalId];
GO
