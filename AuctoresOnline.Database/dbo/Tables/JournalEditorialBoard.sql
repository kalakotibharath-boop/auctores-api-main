CREATE TABLE [dbo].[JournalEditorialBoard]
(
    [EditorialBoardId]  INT             NOT NULL IDENTITY(1,1),
    [JournalId]         INT             NOT NULL,
    -- References Editors.EditorId or EditorProfiles.EditorId depending on EditorRole
    [EditorId]          INT             NOT NULL,
    [EditorRole]        NVARCHAR(30)    NULL
        CONSTRAINT [CK_JournalEditorialBoard_EditorRole] CHECK (
            [EditorRole] IN (N'Manuscript Editor', N'Assistant Editor', N'Consulting Editor', N'Associate Editor', N'Editor Profiles')
        ),

    CONSTRAINT [PK_JournalEditorialBoard] PRIMARY KEY CLUSTERED ([EditorialBoardId] ASC),
    CONSTRAINT [FK_JournalEditorialBoard_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId])
);
GO

CREATE NONCLUSTERED INDEX [IX_JournalEditorialBoard_JournalId]
    ON [dbo].[JournalEditorialBoard] ([JournalId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_JournalEditorialBoard_EditorId]
    ON [dbo].[JournalEditorialBoard] ([EditorId] ASC);
GO
