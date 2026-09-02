CREATE TABLE [dbo].[SubscriptionList]
(
    [SubscriptionId]      INT             NOT NULL IDENTITY(1,1),
    [EmailId]             NVARCHAR(255)   NOT NULL,
    [SubscribedFor]       NVARCHAR(150)   NULL,
    [SubscribedForId]     INT             NOT NULL,
    -- 0 = Unsubscribed, 1 = Subscribed
    [SubscriptionStatus]  TINYINT         NOT NULL CONSTRAINT [DF_SubscriptionList_SubscriptionStatus] DEFAULT (1),
    [CreatedDate]         DATETIME2       NOT NULL CONSTRAINT [DF_SubscriptionList_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]         DATETIME2       NOT NULL,

    CONSTRAINT [PK_SubscriptionList] PRIMARY KEY CLUSTERED ([SubscriptionId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_SubscriptionList_EmailId]
    ON [dbo].[SubscriptionList] ([EmailId] ASC);
GO
