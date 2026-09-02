CREATE TABLE [dbo].[Authors]
(
    [AuthorId]           INT             NOT NULL IDENTITY(1,1),
    [AuthorName]         NVARCHAR(100)   NOT NULL,
    [AuthorEmail]        NVARCHAR(150)   NOT NULL,
    [AuthorPhone]        NVARCHAR(20)    NULL,
    [AuthorCountry]      NVARCHAR(100)   NULL,
    [AuthorDesignation]  NVARCHAR(100)   NULL,
    [AuthorDescription]  NVARCHAR(MAX)   NULL,
    [AuthorImage]        NVARCHAR(255)   NULL,
    [AuthorAddress]      NVARCHAR(MAX)   NULL,
    -- 0 = Inactive, 1 = Active
    [AuthorStatus]       TINYINT         NOT NULL,
    [AuthorPassword]     NVARCHAR(255)   NULL,
    [AuthorCreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_Authors_AuthorCreatedDate] DEFAULT (GETUTCDATE()),
    [AuthorUpdatedDate]  DATETIME2       NULL,

    CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([AuthorId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Authors_AuthorEmail]
    ON [dbo].[Authors] ([AuthorEmail] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Authors_AuthorStatus]
    ON [dbo].[Authors] ([AuthorStatus] ASC);
GO
