CREATE TABLE [dbo].[Journals]
(
    [JournalId]              INT             NOT NULL IDENTITY(1,1),
    [JournalName]            NVARCHAR(150)   NOT NULL,
    [JournalDoi]             NVARCHAR(50)    NULL CONSTRAINT [DF_Journals_JournalDoi] DEFAULT (N'10.31579'),
    [JournalAcceptanceRate]  NVARCHAR(50)    NOT NULL,
    [JournalDecision]        NVARCHAR(50)    NOT NULL,
    [JournalAcceptancePublic] NVARCHAR(50)   NOT NULL,
    [JournalCitescore1]      NVARCHAR(50)    NOT NULL,
    [JournalImpact]          NVARCHAR(50)    NOT NULL,
    [JournalApc]             NVARCHAR(50)    NOT NULL,
    [JournalSnip]            NVARCHAR(50)    NOT NULL,
    [JournalSjr]             NVARCHAR(50)    NOT NULL,
    [JournalCitescore]       NVARCHAR(50)    NOT NULL,
    [Volume]                 INT             NOT NULL,
    [Issue]                  INT             NOT NULL,
    [Year]                   INT             NOT NULL,
    [Email]                  NVARCHAR(255)   NOT NULL,
    [Scholar]                NVARCHAR(MAX)   NOT NULL,
    [IndexedArticle]         NVARCHAR(255)   NOT NULL,
    [IndexingImage]          NVARCHAR(255)   NULL,
    [IndexingUrl]            NVARCHAR(255)   NULL,
    [ImpactFactor]           NVARCHAR(MAX)   NULL,
    [CrossrefUrl]            NVARCHAR(255)   NULL,
    [CrossrefImage]          NVARCHAR(255)   NULL,
    [IssnNumber]             NVARCHAR(100)   NOT NULL,
    [YoutubeLink]            NVARCHAR(255)   NOT NULL,
    [JournalSeoName]         NVARCHAR(150)   NULL,
    [CallForPapers]          NVARCHAR(MAX)   NULL,
    [AimsAndScope]           NVARCHAR(MAX)   NULL,
    [ChiefEditor]            INT             NOT NULL CONSTRAINT [DF_Journals_ChiefEditor] DEFAULT (0),
    [JournalImage]           NVARCHAR(255)   NULL,
    [IsExternalJournal]      INT             NOT NULL CONSTRAINT [DF_Journals_IsExternalJournal] DEFAULT (0),
    -- 0 = Inactive, 1 = Active, 2 = Deleted
    [JournalStatus]          TINYINT         NOT NULL,
    [JournalCreatedDate]     DATETIME2       NOT NULL CONSTRAINT [DF_Journals_JournalCreatedDate] DEFAULT (GETUTCDATE()),
    [JournalUpdatedDate]     DATETIME2       NULL,

    CONSTRAINT [PK_Journals] PRIMARY KEY CLUSTERED ([JournalId] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Journals_JournalStatus]
    ON [dbo].[Journals] ([JournalStatus] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Journals_JournalSeoName]
    ON [dbo].[Journals] ([JournalSeoName] ASC);
GO
