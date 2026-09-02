CREATE TABLE [dbo].[Pages]
(
    [PageId]       INT             NOT NULL IDENTITY(1,1),
    [PageSlug]     NVARCHAR(250)   NOT NULL,
    [PageTitle]    NVARCHAR(MAX)   NOT NULL,
    [CreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_Pages_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]  DATETIME2       NOT NULL,

    CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED ([PageId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Pages_PageSlug]
    ON [dbo].[Pages] ([PageSlug] ASC);
GO
