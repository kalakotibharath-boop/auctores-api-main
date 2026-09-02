CREATE VIEW [dbo].[vw_Manuscripts]
AS
SELECT
    m.[ManuscriptId],
    m.[JournalId],
    m.[ManuscriptSubmittedBy],
    m.[ManuscriptTitle],
    m.[ManuscriptType],
    m.[MessageToEditor],
    m.[ManuscriptFile],
    m.[ManuscriptStatus],
    m.[ManuscriptCreatedDate],
    m.[ManuscriptUpdatedDate],
    j.[JournalName],
    a.[AuthorName]
FROM [dbo].[Manuscripts] m
INNER JOIN [dbo].[Journals] j ON m.[JournalId] = j.[JournalId]
INNER JOIN [dbo].[Authors] a ON m.[ManuscriptSubmittedBy] = a.[AuthorId];
GO
