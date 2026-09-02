CREATE TABLE [dbo].[MemberedIn]
(
    [Id]           INT             NOT NULL IDENTITY(1,1),
    [JournalId]    INT             NOT NULL,
    [Name]         NVARCHAR(200)   NOT NULL,
    [Image]        NVARCHAR(255)   NOT NULL,
    [Url]          NVARCHAR(250)   NOT NULL,
    [Status]       INT             NOT NULL CONSTRAINT [DF_MemberedIn_Status] DEFAULT (1),
    [CreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_MemberedIn_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]  DATETIME2       NOT NULL,

    CONSTRAINT [PK_MemberedIn] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemberedIn_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId])
);
GO

CREATE NONCLUSTERED INDEX [IX_MemberedIn_JournalId]
    ON [dbo].[MemberedIn] ([JournalId] ASC);
GO
