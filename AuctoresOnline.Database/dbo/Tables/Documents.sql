CREATE TABLE [dbo].[Documents]
(
    [DocId]        BIGINT          NOT NULL IDENTITY(1,1),
    [Title]        NVARCHAR(MAX)   NOT NULL,
    [PageSlug]     NVARCHAR(255)   NOT NULL,
    [DocData]      NVARCHAR(MAX)   NOT NULL,
    [JournalId]    BIGINT          NULL,
    [SeoKeywords]  NVARCHAR(MAX)   NOT NULL,
    [Type]         NVARCHAR(100)   NULL,
    [Status]       INT             NOT NULL CONSTRAINT [DF_Documents_Status] DEFAULT (1),
    [CreatedBy]    INT             NULL,
    [CreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_Documents_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]  DATETIME2       NOT NULL,

    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([DocId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Documents_JournalId]
    ON [dbo].[Documents] ([JournalId] ASC)
    WHERE [JournalId] IS NOT NULL;
GO

CREATE NONCLUSTERED INDEX [IX_Documents_PageSlug]
    ON [dbo].[Documents] ([PageSlug] ASC);
GO
