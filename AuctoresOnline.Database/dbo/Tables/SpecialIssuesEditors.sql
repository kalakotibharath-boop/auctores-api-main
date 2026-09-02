CREATE TABLE [dbo].[SpecialIssuesEditors]
(
    [IssueEditorId]  BIGINT      NOT NULL IDENTITY(1,1),
    [EditorId]       BIGINT      NOT NULL,
    [IssueId]        BIGINT      NOT NULL,
    [CreatedDate]    DATETIME2   NOT NULL CONSTRAINT [DF_SpecialIssuesEditors_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]    DATETIME2   NOT NULL,

    CONSTRAINT [PK_SpecialIssuesEditors] PRIMARY KEY CLUSTERED ([IssueEditorId] ASC),
    CONSTRAINT [FK_SpecialIssuesEditors_SpecialIssues] FOREIGN KEY ([IssueId])
        REFERENCES [dbo].[SpecialIssues] ([IssueId])
);
GO

CREATE NONCLUSTERED INDEX [IX_SpecialIssuesEditors_IssueId]
    ON [dbo].[SpecialIssuesEditors] ([IssueId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_SpecialIssuesEditors_EditorId]
    ON [dbo].[SpecialIssuesEditors] ([EditorId] ASC);
GO
