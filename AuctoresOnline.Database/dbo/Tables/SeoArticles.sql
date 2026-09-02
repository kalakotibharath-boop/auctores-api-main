CREATE TABLE [dbo].[SeoArticles]
(
    [Id]           BIGINT          NOT NULL IDENTITY(1,1),
    [ArticleName]  NVARCHAR(MAX)   NOT NULL,
    [Type]         NVARCHAR(255)   NOT NULL,
    [ArticleId]    INT             NOT NULL,

    CONSTRAINT [PK_SeoArticles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_SeoArticles_ArticleId]
    ON [dbo].[SeoArticles] ([ArticleId] ASC);
GO
