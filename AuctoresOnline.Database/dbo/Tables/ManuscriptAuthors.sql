CREATE TABLE [dbo].[ManuscriptAuthors]
(
    [ManuscriptAuthorId]           INT             NOT NULL IDENTITY(1,1),
    [ManuscriptId]                 INT             NOT NULL,
    [AuthorId]                     INT             NOT NULL,
    [ManuscriptAuthorType]         NVARCHAR(20)    NOT NULL
        CONSTRAINT [DF_ManuscriptAuthors_ManuscriptAuthorType] DEFAULT (N'Co Author')
        CONSTRAINT [CK_ManuscriptAuthors_ManuscriptAuthorType] CHECK (
            [ManuscriptAuthorType] IN (N'Main Author', N'Co Author')
        ),
    [ManuscriptAuthorStatus]       TINYINT         NOT NULL CONSTRAINT [DF_ManuscriptAuthors_ManuscriptAuthorStatus] DEFAULT (1),
    [ManuscriptAuthorCreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_ManuscriptAuthors_ManuscriptAuthorCreatedDate] DEFAULT (GETUTCDATE()),
    [ManuscriptAuthorUpdatedDate]  DATETIME2       NULL,

    CONSTRAINT [PK_ManuscriptAuthors] PRIMARY KEY CLUSTERED ([ManuscriptAuthorId] ASC),
    CONSTRAINT [FK_ManuscriptAuthors_Manuscripts] FOREIGN KEY ([ManuscriptId])
        REFERENCES [dbo].[Manuscripts] ([ManuscriptId]),
    CONSTRAINT [FK_ManuscriptAuthors_Authors] FOREIGN KEY ([AuthorId])
        REFERENCES [dbo].[Authors] ([AuthorId])
);
GO

CREATE NONCLUSTERED INDEX [IX_ManuscriptAuthors_ManuscriptId]
    ON [dbo].[ManuscriptAuthors] ([ManuscriptId] ASC);
GO
