CREATE VIEW [dbo].[vw_SubscriptionList]
AS
SELECT
    sl.[SubscriptionId],
    sl.[EmailId],
    sl.[SubscribedFor],
    sl.[SubscribedForId],
    sl.[SubscriptionStatus],
    j.[JournalName],
    sl.[CreatedDate],
    sl.[UpdatedDate]
FROM [dbo].[SubscriptionList] sl
LEFT JOIN [dbo].[Journals] j ON sl.[SubscribedForId] = j.[JournalId];
GO
