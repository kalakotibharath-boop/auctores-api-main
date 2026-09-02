CREATE TABLE [dbo].[JournalSections]
(
    [SectionId]    INT             NOT NULL IDENTITY(1,1),
    [JournalId]    INT             NOT NULL,
    [SectionName]  NVARCHAR(100)   NOT NULL,

    CONSTRAINT [PK_JournalSections] PRIMARY KEY CLUSTERED ([SectionId] ASC),
    CONSTRAINT [FK_JournalSections_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId])
);
GO

CREATE NONCLUSTERED INDEX [IX_JournalSections_JournalId]
    ON [dbo].[JournalSections] ([JournalId] ASC);
GO
