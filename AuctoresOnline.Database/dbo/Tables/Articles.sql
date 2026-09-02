CREATE TABLE [dbo].[Articles]
(
    [ArticleId]                 BIGINT          NOT NULL IDENTITY(1,1),
    [ArticleViews]              INT             NOT NULL CONSTRAINT [DF_Articles_ArticleViews] DEFAULT (1),
    [ArticleDownloads]          INT             NOT NULL CONSTRAINT [DF_Articles_ArticleDownloads] DEFAULT (1),
    -- Descriptive article type label e.g. 'Review Article', 'Case Report', 'Research Article'
    [ArticleFor]                NVARCHAR(100)   NOT NULL,
    [JournalId]                 INT             NOT NULL,
    [VolumeNo]                  INT             NOT NULL,
    [IssueNo]                   INT             NOT NULL,
    -- 0 = Article In Press, 1 = Current Issue, 2 = Archive
    [ArticleType]               TINYINT         NOT NULL,
    [ArticleName]               NVARCHAR(500)   NOT NULL,
    [ArticleSeoName]            NVARCHAR(500)   NOT NULL,
    [ArticleDoi]                NVARCHAR(100)   NULL,
    [CorrespondingAuthor]       NVARCHAR(MAX)   NOT NULL,
    [ReceivedDate]              DATE            NULL,
    [AcceptedDate]              DATE            NULL,
    [PublishedDate]             DATE            NULL,
    [Citation]                  NVARCHAR(1000)  NULL,
    [Copyright]                 NVARCHAR(1000)  NULL,
    [ArticleInformation]        NVARCHAR(MAX)   NULL,
    [ArticleAbstract]           NVARCHAR(MAX)   NULL,
    [DisplayArticleInformation] NVARCHAR(MAX)   NULL,
    [DisplayArticleAbstract]    NVARCHAR(MAX)   NULL,
    [AddressDesc]               NVARCHAR(MAX)   NULL,
    [AbstractKeywords]          NVARCHAR(500)   NULL,
    [ArticlePdf]                NVARCHAR(255)   NOT NULL,
    -- 0 = Inactive, 1 = Active
    [ArticleStatus]             TINYINT         NOT NULL CONSTRAINT [DF_Articles_ArticleStatus] DEFAULT (1),
    [ArticleCreatedDate]        DATETIME2       NOT NULL CONSTRAINT [DF_Articles_ArticleCreatedDate] DEFAULT (GETUTCDATE()),
    [ArticleUpdatedDate]        DATETIME2       NULL,

    CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED ([ArticleId] ASC),
    CONSTRAINT [FK_Articles_Journals] FOREIGN KEY ([JournalId])
        REFERENCES [dbo].[Journals] ([JournalId])
);
GO

CREATE NONCLUSTERED INDEX [IX_Articles_JournalId]
    ON [dbo].[Articles] ([JournalId] ASC)
    INCLUDE ([ArticleType], [ArticleStatus], [PublishedDate], [VolumeNo], [IssueNo]);
GO

CREATE NONCLUSTERED INDEX [IX_Articles_JournalVolumeIssue]
    ON [dbo].[Articles] ([JournalId] ASC, [VolumeNo] ASC, [IssueNo] ASC)
    INCLUDE ([ArticleType], [ArticleStatus]);
GO

CREATE NONCLUSTERED INDEX [IX_Articles_ArticleType_Status]
    ON [dbo].[Articles] ([ArticleType] ASC, [ArticleStatus] ASC)
    INCLUDE ([JournalId], [PublishedDate]);
GO

CREATE NONCLUSTERED INDEX [IX_Articles_PublishedDate]
    ON [dbo].[Articles] ([PublishedDate] DESC)
    INCLUDE ([JournalId], [ArticleStatus]);
GO

CREATE NONCLUSTERED INDEX [IX_Articles_ArticleSeoName]
    ON [dbo].[Articles] ([ArticleSeoName] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Articles_ArticleDoi]
    ON [dbo].[Articles] ([ArticleDoi] ASC)
    WHERE [ArticleDoi] IS NOT NULL;
GO
