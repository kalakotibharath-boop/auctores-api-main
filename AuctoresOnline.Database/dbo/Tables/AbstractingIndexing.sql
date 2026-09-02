CREATE TABLE [dbo].[AbstractingIndexing]
(
    [AbstractionId]    BIGINT          NOT NULL IDENTITY(1,1),
    [JournalId]        INT             NOT NULL,
    [AbstractionUrl]   NVARCHAR(MAX)   NOT NULL,
    [AbstractionTitle] NVARCHAR(255)   NOT NULL,
    [Status]           TINYINT         NOT NULL CONSTRAINT [DF_AbstractingIndexing_Status] DEFAULT (1),
    [CreatedDate]      DATETIME2       NOT NULL CONSTRAINT [DF_AbstractingIndexing_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]      DATETIME2       NULL,

    CONSTRAINT [PK_AbstractingIndexing] PRIMARY KEY CLUSTERED ([AbstractionId] ASC),
    CONSTRAINT [FK_AbstractingIndexing_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId])
);
GO

CREATE NONCLUSTERED INDEX [IX_AbstractingIndexing_JournalId]
    ON [dbo].[AbstractingIndexing] ([JournalId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_AbstractingIndexing_Status]
    ON [dbo].[AbstractingIndexing] ([Status] ASC);
GO
