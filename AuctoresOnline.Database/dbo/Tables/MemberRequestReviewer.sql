CREATE TABLE [dbo].[MemberRequestReviewer]
(
    [Id]           BIGINT          NOT NULL IDENTITY(1,1),
    [Name]         NVARCHAR(255)   NOT NULL,
    [Email]        NVARCHAR(255)   NOT NULL,
    [PhoneNumber]  NVARCHAR(20)    NOT NULL,
    [Designation]  NVARCHAR(255)   NOT NULL,
    [Country]      NVARCHAR(255)   NOT NULL,
    [BioData]      NVARCHAR(MAX)   NOT NULL,
    [Files]        NVARCHAR(255)   NOT NULL,
    [Type]         INT             NOT NULL,
    [JournalId]    INT             NOT NULL,
    [CreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_MemberRequestReviewer_CreatedDate] DEFAULT (GETUTCDATE()),

    CONSTRAINT [PK_MemberRequestReviewer] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_MemberRequestReviewer_JournalId]
    ON [dbo].[MemberRequestReviewer] ([JournalId] ASC);
GO
