CREATE TABLE [dbo].[Reviewers]
(
    [ReviewerId]            INT             NOT NULL IDENTITY(1,1),
    [ReviewerName]          NVARCHAR(100)   NOT NULL,
    [ReviewerEmail]         NVARCHAR(150)   NOT NULL,
    [ReviewerPhone]         NVARCHAR(20)    NULL,
    [ReviewerCountry]       NVARCHAR(50)    NULL,
    [ReviewerDesignation]   NVARCHAR(100)   NULL,
    [ReviewerImage]         NVARCHAR(255)   NULL,
    [ReviewerAddress]       NVARCHAR(MAX)   NULL,
    [ReviewerDescription]   NVARCHAR(MAX)   NULL,
    -- 0 = Inactive, 1 = Active
    [ReviewerStatus]        TINYINT         NOT NULL,
    [ReviewerPassword]      NVARCHAR(255)   NOT NULL,
    [ReviewerCreatedDate]   DATETIME2       NOT NULL CONSTRAINT [DF_Reviewers_ReviewerCreatedDate] DEFAULT (GETUTCDATE()),
    [ReviewerUpdatedDate]   DATETIME2       NULL,

    CONSTRAINT [PK_Reviewers] PRIMARY KEY CLUSTERED ([ReviewerId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Reviewers_ReviewerEmail]
    ON [dbo].[Reviewers] ([ReviewerEmail] ASC);
GO
