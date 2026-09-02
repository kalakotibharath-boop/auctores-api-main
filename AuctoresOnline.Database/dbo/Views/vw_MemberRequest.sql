CREATE VIEW [dbo].[vw_MemberRequest]
AS
SELECT
    mrr.[Id]           AS [RequestId],
    mrr.[Name],
    mrr.[Email],
    mrr.[PhoneNumber],
    mrr.[Designation],
    mrr.[Country],
    mrr.[Type],
    mrr.[CreatedDate],
    j.[JournalName],
    j.[JournalStatus]
FROM [dbo].[MemberRequestReviewer] mrr
LEFT JOIN [dbo].[Journals] j ON mrr.[JournalId] = j.[JournalId];
GO
