CREATE VIEW [dbo].[vw_AbstractingIndexingDetail]
AS
SELECT
    ai.[AbstractionId],
    ai.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[JournalStatus],
    ai.[AbstractionUrl],
    ai.[AbstractionTitle],
    ai.[Status]  AS [AbstractionStatus],
    ai.[CreatedDate],
    ai.[UpdatedDate]
FROM
    [dbo].[AbstractingIndexing] ai
    INNER JOIN [dbo].[Journals] j ON ai.[JournalId] = j.[JournalId];
GO
