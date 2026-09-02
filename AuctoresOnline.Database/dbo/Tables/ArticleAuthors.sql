CREATE TABLE [dbo].[ArticleAuthors]
(
    [ArticleAuthorId]  BIGINT          NOT NULL IDENTITY(1,1),
    [ArticleId]        BIGINT          NOT NULL,
    [AuthorName]       NVARCHAR(255)   NOT NULL,
    [AuthorNo]         TINYINT         NOT NULL,
    [AuthorSubhead]    NVARCHAR(5)     NOT NULL,
    [AuthorEmail]      NVARCHAR(255)   NULL,
    [AuthorOrcid]      NVARCHAR(255)   NULL,
    [AuthorAddress]    NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_ArticleAuthors] PRIMARY KEY CLUSTERED ([ArticleAuthorId] ASC),
    CONSTRAINT [FK_ArticleAuthors_Articles] FOREIGN KEY ([ArticleId])
        REFERENCES [dbo].[Articles] ([ArticleId])
);
GO

CREATE NONCLUSTERED INDEX [IX_ArticleAuthors_ArticleId]
    ON [dbo].[ArticleAuthors] ([ArticleId] ASC);
GO
