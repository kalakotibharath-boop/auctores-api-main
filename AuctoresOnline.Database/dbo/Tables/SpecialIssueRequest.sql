CREATE TABLE [dbo].[SpecialIssueRequest]
(
    [RequestId]    INT             NOT NULL IDENTITY(1,1),
    [JournalId]    NVARCHAR(200)   NOT NULL,
    [ArticleType]  NVARCHAR(100)   NOT NULL,
    [Title]        NVARCHAR(200)   NOT NULL,
    [Name]         NVARCHAR(100)   NOT NULL,
    [Country]      NVARCHAR(50)    NOT NULL,
    [EmailId]      NVARCHAR(50)    NOT NULL,
    [PhoneNumber]  NVARCHAR(20)    NOT NULL,
    [Address]      NVARCHAR(MAX)   NOT NULL,
    [FileLink]     NVARCHAR(100)   NOT NULL,
    [Description]  NVARCHAR(MAX)   NOT NULL,
    [Status]       NVARCHAR(50)    NOT NULL,
    [CreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_SpecialIssueRequest_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]  DATETIME2       NOT NULL,

    CONSTRAINT [PK_SpecialIssueRequest] PRIMARY KEY CLUSTERED ([RequestId] ASC)
);
GO
