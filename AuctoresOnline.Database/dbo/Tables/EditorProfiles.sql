CREATE TABLE [dbo].[EditorProfiles]
(
    [EditorId]            INT             NOT NULL IDENTITY(1,1),
    [EditorName]          NVARCHAR(100)   NOT NULL,
    [EditorEmail]         NVARCHAR(150)   NOT NULL,
    [EditorPhone]         NVARCHAR(20)    NULL,
    [EditorDesignation]   NVARCHAR(100)   NULL,
    [EditorImage]         NVARCHAR(255)   NULL,
    [EditorDescription]   NVARCHAR(MAX)   NULL,
    -- 0 = Inactive, 1 = Active
    [EditorStatus]        TINYINT         NOT NULL,
    [EditorPassword]      NVARCHAR(255)   NOT NULL,
    [EditorCreatedDate]   DATETIME2       NOT NULL CONSTRAINT [DF_EditorProfiles_EditorCreatedDate] DEFAULT (GETUTCDATE()),
    [EditorUpdatedDate]   DATETIME2       NULL,

    CONSTRAINT [PK_EditorProfiles] PRIMARY KEY CLUSTERED ([EditorId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_EditorProfiles_EditorEmail]
    ON [dbo].[EditorProfiles] ([EditorEmail] ASC);
GO
