CREATE VIEW [dbo].[vw_MemberedIn]
AS
SELECT
    mi.[Id],
    mi.[JournalId],
    j.[JournalName],
    mi.[Name],
    mi.[Image],
    mi.[Url],
    mi.[Status],
    mi.[CreatedDate],
    mi.[UpdatedDate]
FROM [dbo].[MemberedIn] mi
LEFT JOIN [dbo].[Journals] j ON mi.[JournalId] = j.[JournalId];
GO
