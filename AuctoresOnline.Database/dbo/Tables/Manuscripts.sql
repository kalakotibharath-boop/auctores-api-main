CREATE TABLE [dbo].[Manuscripts]
(
    [ManuscriptId]           INT             NOT NULL IDENTITY(1,1),
    [JournalId]              INT             NOT NULL,
    [ManuscriptSubmittedBy]  INT             NOT NULL,
    [ManuscriptTitle]        NVARCHAR(150)   NOT NULL,
    [ManuscriptType]         NVARCHAR(50)    NOT NULL,
    [MessageToEditor]        NVARCHAR(MAX)   NOT NULL,
    [ManuscriptFile]         NVARCHAR(150)   NOT NULL,
    -- 0 = Pending, 1 = Accepted, 2 = Published, 3 = Rejected
    [ManuscriptStatus]       TINYINT         NOT NULL,
    [ManuscriptCreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_Manuscripts_ManuscriptCreatedDate] DEFAULT (GETUTCDATE()),
    [ManuscriptUpdatedDate]  DATETIME2       NULL,

    CONSTRAINT [PK_Manuscripts] PRIMARY KEY CLUSTERED ([ManuscriptId] ASC),
    CONSTRAINT [FK_Manuscripts_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId]),
    CONSTRAINT [FK_Manuscripts_Authors] FOREIGN KEY ([ManuscriptSubmittedBy])
        REFERENCES [dbo].[Authors] ([AuthorId])
);
GO

CREATE NONCLUSTERED INDEX [IX_Manuscripts_JournalId]
    ON [dbo].[Manuscripts] ([JournalId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Manuscripts_ManuscriptSubmittedBy]
    ON [dbo].[Manuscripts] ([ManuscriptSubmittedBy] ASC);
GO
