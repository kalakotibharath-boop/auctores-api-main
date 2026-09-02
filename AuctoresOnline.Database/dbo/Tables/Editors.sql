CREATE TABLE [dbo].[Editors]
(
    [EditorId]              INT             NOT NULL IDENTITY(1,1),
    [EditorName]            NVARCHAR(100)   NOT NULL,
    [EditorDesignation]     NVARCHAR(255)   NOT NULL,
    [Qualification]         NVARCHAR(255)   NOT NULL,
    [ProfileImage]          NVARCHAR(255)   NOT NULL,
    [EditorEmail]           NVARCHAR(150)   NOT NULL,
    [EditorPhone]           NVARCHAR(20)    NULL,
    [EditorAddress]         NVARCHAR(MAX)   NULL,
    [EditorBiography]       NVARCHAR(MAX)   NOT NULL,
    [EditorResearch]        NVARCHAR(MAX)   NOT NULL,
    -- 0 = Inactive, 1 = Active
    [EditorStatus]          TINYINT         NOT NULL,
    [EditorPassword]        NVARCHAR(255)   NOT NULL,
    [EditorOrcid]           NVARCHAR(200)   NULL,
    [EditorGoogleScholar]   NVARCHAR(250)   NULL,
    [EditorWebsite]         NVARCHAR(250)   NULL,
    [EditorCreatedDate]     DATETIME2       NOT NULL CONSTRAINT [DF_Editors_EditorCreatedDate] DEFAULT (GETUTCDATE()),
    [EditorUpdatedDate]     DATETIME2       NULL,

    CONSTRAINT [PK_Editors] PRIMARY KEY CLUSTERED ([EditorId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Editors_EditorEmail]
    ON [dbo].[Editors] ([EditorEmail] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Editors_EditorStatus]
    ON [dbo].[Editors] ([EditorStatus] ASC);
GO
