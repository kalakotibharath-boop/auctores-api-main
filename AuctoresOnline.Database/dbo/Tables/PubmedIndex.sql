CREATE TABLE [dbo].[PubmedIndex]
(
    [PubmedIndexId]  INT             NOT NULL IDENTITY(1,1),
    [JournalId]      INT             NOT NULL,
    [IndexText]      NVARCHAR(MAX)   NOT NULL,
    [PubmedUrl]      NVARCHAR(255)   NOT NULL,
    [PmcUrl]         NVARCHAR(255)   NOT NULL,
    [CreatedDate]    DATETIME2       NOT NULL CONSTRAINT [DF_PubmedIndex_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]    DATETIME2       NOT NULL,

    CONSTRAINT [PK_PubmedIndex] PRIMARY KEY CLUSTERED ([PubmedIndexId] ASC),
    CONSTRAINT [FK_PubmedIndex_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId])
);
GO

CREATE NONCLUSTERED INDEX [IX_PubmedIndex_JournalId]
    ON [dbo].[PubmedIndex] ([JournalId] ASC);
GO
