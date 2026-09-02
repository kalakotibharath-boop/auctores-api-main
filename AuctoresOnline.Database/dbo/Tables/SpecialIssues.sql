CREATE TABLE [dbo].[SpecialIssues]
(
    [IssueId]           BIGINT          NOT NULL IDENTITY(1,1),
    [Title]             NVARCHAR(MAX)   NOT NULL,
    [JournalId]         BIGINT          NOT NULL,
    [Slug]              NVARCHAR(200)   NOT NULL,
    [DeadlineDate]      DATE            NOT NULL,
    [ViewCount]         INT             NOT NULL,
    [IssnNumber]        NVARCHAR(100)   NOT NULL,
    [CitiesScore]       NVARCHAR(50)    NOT NULL,
    [ImpactFactor]      NVARCHAR(50)    NOT NULL,
    [FileLink]          NVARCHAR(50)    NOT NULL,
    [IssueData]         NVARCHAR(MAX)   NOT NULL,
    [IssueDataDisplay]  NVARCHAR(MAX)   NOT NULL,
    [Keywords]          NVARCHAR(MAX)   NOT NULL,
    [Benefits]          NVARCHAR(MAX)   NOT NULL,
    [CreatedDate]       DATETIME2       NOT NULL CONSTRAINT [DF_SpecialIssues_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]       DATETIME2       NOT NULL,
    [Status]            INT             NOT NULL,

    CONSTRAINT [PK_SpecialIssues] PRIMARY KEY CLUSTERED ([IssueId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_SpecialIssues_JournalId]
    ON [dbo].[SpecialIssues] ([JournalId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_SpecialIssues_Slug]
    ON [dbo].[SpecialIssues] ([Slug] ASC);
GO
