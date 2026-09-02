CREATE TABLE [dbo].[OldArticles]
(
    [Id]              INT             NOT NULL IDENTITY(1,1),
    [ArticleName]     NVARCHAR(255)   NOT NULL,
    [ArticleSeoName]  NVARCHAR(255)   NOT NULL,
    [ArticleId]       INT             NOT NULL,
    [Type]            INT             NOT NULL,
    [CreatedDate]     DATETIME2       NOT NULL CONSTRAINT [DF_OldArticles_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]     DATETIME2       NOT NULL,

    CONSTRAINT [PK_OldArticles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
