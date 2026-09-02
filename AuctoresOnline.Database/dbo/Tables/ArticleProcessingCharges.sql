CREATE TABLE [dbo].[ArticleProcessingCharges]
(
    [ApcId]        BIGINT      NOT NULL IDENTITY(1,1),
    [JournalId]    INT         NOT NULL,
    [ApcAmount]    INT         NOT NULL,
    [CreatedDate]  DATETIME2   NOT NULL CONSTRAINT [DF_ArticleProcessingCharges_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]  DATETIME2   NULL,

    CONSTRAINT [PK_ArticleProcessingCharges] PRIMARY KEY CLUSTERED ([ApcId] ASC),
    CONSTRAINT [FK_ArticleProcessingCharges_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId])
);
GO

CREATE NONCLUSTERED INDEX [IX_ArticleProcessingCharges_JournalId]
    ON [dbo].[ArticleProcessingCharges] ([JournalId] ASC);
GO
