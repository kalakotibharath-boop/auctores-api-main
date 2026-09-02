CREATE TABLE [dbo].[ArticleDetails]
(
    [DetailsId]                  BIGINT          NOT NULL IDENTITY(1,1),
    [DetailsArticleId]           BIGINT          NOT NULL,
    [DetailsHeading]             NVARCHAR(100)   NOT NULL,
    [DetailsDescription]         NVARCHAR(MAX)   NULL,
    [DisplayDetailsDescription]  NVARCHAR(MAX)   NULL,
    [CreatedDate]                DATETIME2       NOT NULL CONSTRAINT [DF_ArticleDetails_CreatedDate] DEFAULT (GETUTCDATE()),

    CONSTRAINT [PK_ArticleDetails] PRIMARY KEY CLUSTERED ([DetailsId] ASC),
    CONSTRAINT [FK_ArticleDetails_Articles] FOREIGN KEY ([DetailsArticleId])
        REFERENCES [dbo].[Articles] ([ArticleId])
);
GO

CREATE NONCLUSTERED INDEX [IX_ArticleDetails_DetailsArticleId]
    ON [dbo].[ArticleDetails] ([DetailsArticleId] ASC);
GO
