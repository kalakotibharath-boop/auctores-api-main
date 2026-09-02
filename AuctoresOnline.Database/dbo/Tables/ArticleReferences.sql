CREATE TABLE [dbo].[ArticleReferences]
(
    [ReferenceId]             BIGINT          NOT NULL IDENTITY(1,1),
    [ReferenceArticleId]      BIGINT          NOT NULL,
    [ReferenceData]           NVARCHAR(MAX)   NOT NULL,
    [ReferenceGoogleLink]     NVARCHAR(MAX)   NOT NULL,
    [ReferencePublisherLink]  NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_ArticleReferences] PRIMARY KEY CLUSTERED ([ReferenceId] ASC),
    CONSTRAINT [FK_ArticleReferences_Articles] FOREIGN KEY ([ReferenceArticleId])
        REFERENCES [dbo].[Articles] ([ArticleId])
);
GO

CREATE NONCLUSTERED INDEX [IX_ArticleReferences_ReferenceArticleId]
    ON [dbo].[ArticleReferences] ([ReferenceArticleId] ASC);
GO
