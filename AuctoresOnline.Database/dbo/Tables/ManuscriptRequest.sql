CREATE TABLE [dbo].[ManuscriptRequest]
(
    [RequestId]       INT             NOT NULL IDENTITY(1,1),
    [FirstName]       NVARCHAR(255)   NOT NULL,
    [LastName]        NVARCHAR(255)   NOT NULL,
    [Name]            NVARCHAR(255)   NOT NULL,
    [Email]           NVARCHAR(255)   NOT NULL,
    [Phone]           NVARCHAR(20)    NULL,
    [Country]         NVARCHAR(100)   NOT NULL,
    [Orcid]           NVARCHAR(255)   NULL,
    [Address]         NVARCHAR(MAX)   NOT NULL,
    [JournalId]       INT             NULL,
    [ManuscriptTitle] NVARCHAR(255)   NOT NULL,
    [ArticleType]     NVARCHAR(255)   NOT NULL,
    [Abstract]        NVARCHAR(MAX)   NOT NULL,
    [Keywords]        NVARCHAR(MAX)   NOT NULL,
    [BioData]         NVARCHAR(MAX)   NOT NULL,
    [Files]           NVARCHAR(MAX)   NOT NULL,
    [CoverLetter]     NVARCHAR(MAX)   NULL,
    -- 1 = Requested, 2 = Reached, 3 = Approved, 4 = Approved
    [Status]          INT             NOT NULL CONSTRAINT [DF_ManuscriptRequest_Status] DEFAULT (1),
    [CreatedDate]     DATETIME2       NOT NULL CONSTRAINT [DF_ManuscriptRequest_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]     DATETIME2       NOT NULL,

    CONSTRAINT [PK_ManuscriptRequest] PRIMARY KEY CLUSTERED ([RequestId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_ManuscriptRequest_JournalId]
    ON [dbo].[ManuscriptRequest] ([JournalId] ASC)
    WHERE [JournalId] IS NOT NULL;
GO
