CREATE VIEW [dbo].[vw_PubmedIndex]
AS
SELECT
    pi.[PubmedIndexId],
    pi.[JournalId],
    pi.[IndexText],
    pi.[PubmedUrl],
    pi.[PmcUrl],
    pi.[CreatedDate],
    pi.[UpdatedDate],
    j.[JournalName],
    j.[JournalSeoName]
FROM [dbo].[PubmedIndex] pi
LEFT JOIN [dbo].[Journals] j ON pi.[JournalId] = j.[JournalId];
GO
