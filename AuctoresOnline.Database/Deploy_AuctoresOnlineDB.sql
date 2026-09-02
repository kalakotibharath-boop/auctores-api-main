-- =============================================================================
--  AuctoresOnline Database Deployment Script
--  Generated: 2026-06-20
--  Target:    SQL Server 2016+ (Express / Standard / Enterprise)
--  Usage:     Run once on a new server, or re-run safely on an existing DB
--             (tables created only when missing; views use CREATE OR ALTER)
-- =============================================================================

SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

USE [master];
GO

-- ---------------------------------------------------------------------------
-- 1. Create database if it doesn't exist
-- ---------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'AuctoresOnlineDB')
BEGIN
    CREATE DATABASE [AuctoresOnlineDB];
    PRINT 'Database AuctoresOnlineDB created.';
END
GO

USE [AuctoresOnlineDB];
GO

-- ===========================================================================
--  TABLES  (created in foreign-key dependency order)
-- ===========================================================================

-- ---------------------------------------------------------------------------
-- Admin
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Admin]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Admin]
    (
        [Id]          INT             NOT NULL IDENTITY(1,1),
        [Name]        NVARCHAR(200)   NOT NULL,
        [Email]       NVARCHAR(200)   NOT NULL,
        [Mobile]      NVARCHAR(100)   NOT NULL,
        [Username]    NVARCHAR(100)   NOT NULL,
        [Password]    NVARCHAR(256)   NOT NULL,
        -- 0 = auctores-admin, 1 = editor, 2 = super-admin
        [RoleType]    SMALLINT        NOT NULL CONSTRAINT [DF_Admin_RoleType]    DEFAULT (0),
        [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Admin_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate] DATETIME2       NULL,

        CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table Admin created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UIX_Admin_Username' AND object_id = OBJECT_ID(N'[dbo].[Admin]'))
    CREATE UNIQUE NONCLUSTERED INDEX [UIX_Admin_Username] ON [dbo].[Admin] ([Username] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UIX_Admin_Email' AND object_id = OBJECT_ID(N'[dbo].[Admin]'))
    CREATE UNIQUE NONCLUSTERED INDEX [UIX_Admin_Email] ON [dbo].[Admin] ([Email] ASC);
GO

-- ---------------------------------------------------------------------------
-- Journals  (no FK – many other tables depend on this)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Journals]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Journals]
    (
        [JournalId]               INT             NOT NULL IDENTITY(1,1),
        [JournalName]             NVARCHAR(150)   NOT NULL,
        [JournalDoi]              NVARCHAR(50)    NULL CONSTRAINT [DF_Journals_JournalDoi] DEFAULT (N'10.31579'),
        [JournalAcceptanceRate]   NVARCHAR(50)    NOT NULL,
        [JournalDecision]         NVARCHAR(50)    NOT NULL,
        [JournalAcceptancePublic] NVARCHAR(50)    NOT NULL,
        [JournalCitescore1]       NVARCHAR(50)    NOT NULL,
        [JournalImpact]           NVARCHAR(50)    NOT NULL,
        [JournalApc]              NVARCHAR(50)    NOT NULL,
        [JournalSnip]             NVARCHAR(50)    NOT NULL,
        [JournalSjr]              NVARCHAR(50)    NOT NULL,
        [JournalCitescore]        NVARCHAR(50)    NOT NULL,
        [Volume]                  INT             NOT NULL,
        [Issue]                   INT             NOT NULL,
        [Year]                    INT             NOT NULL,
        [Email]                   NVARCHAR(255)   NOT NULL,
        [Scholar]                 NVARCHAR(MAX)   NOT NULL,
        [IndexedArticle]          NVARCHAR(255)   NOT NULL,
        [IndexingImage]           NVARCHAR(255)   NULL,
        [IndexingUrl]             NVARCHAR(255)   NULL,
        [ImpactFactor]            NVARCHAR(MAX)   NULL,
        [CrossrefUrl]             NVARCHAR(255)   NULL,
        [CrossrefImage]           NVARCHAR(255)   NULL,
        [IssnNumber]              NVARCHAR(100)   NOT NULL,
        [YoutubeLink]             NVARCHAR(255)   NOT NULL,
        [JournalSeoName]          NVARCHAR(150)   NULL,
        [CallForPapers]           NVARCHAR(MAX)   NULL,
        [AimsAndScope]            NVARCHAR(MAX)   NULL,
        [ChiefEditor]             INT             NOT NULL CONSTRAINT [DF_Journals_ChiefEditor]          DEFAULT (0),
        [JournalImage]            NVARCHAR(255)   NULL,
        [IsExternalJournal]       INT             NOT NULL CONSTRAINT [DF_Journals_IsExternalJournal]    DEFAULT (0),
        -- 0 = Inactive, 1 = Active, 2 = Deleted
        [JournalStatus]           TINYINT         NOT NULL,
        [JournalCreatedDate]      DATETIME2       NOT NULL CONSTRAINT [DF_Journals_JournalCreatedDate]   DEFAULT (GETUTCDATE()),
        [JournalUpdatedDate]      DATETIME2       NULL,

        CONSTRAINT [PK_Journals] PRIMARY KEY CLUSTERED ([JournalId] ASC)
    );
    PRINT 'Table Journals created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Journals_JournalStatus' AND object_id = OBJECT_ID(N'[dbo].[Journals]'))
    CREATE NONCLUSTERED INDEX [IX_Journals_JournalStatus] ON [dbo].[Journals] ([JournalStatus] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Journals_JournalSeoName' AND object_id = OBJECT_ID(N'[dbo].[Journals]'))
    CREATE NONCLUSTERED INDEX [IX_Journals_JournalSeoName] ON [dbo].[Journals] ([JournalSeoName] ASC);
GO

-- ---------------------------------------------------------------------------
-- Editors
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Editors]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Editors]
    (
        [EditorId]            INT             NOT NULL IDENTITY(1,1),
        [EditorName]          NVARCHAR(100)   NOT NULL,
        [EditorDesignation]   NVARCHAR(255)   NOT NULL,
        [Qualification]       NVARCHAR(255)   NOT NULL,
        [ProfileImage]        NVARCHAR(255)   NOT NULL,
        [EditorEmail]         NVARCHAR(150)   NOT NULL,
        [EditorPhone]         NVARCHAR(20)    NULL,
        [EditorAddress]       NVARCHAR(MAX)   NULL,
        [EditorBiography]     NVARCHAR(MAX)   NOT NULL,
        [EditorResearch]      NVARCHAR(MAX)   NOT NULL,
        -- 0 = Inactive, 1 = Active
        [EditorStatus]        TINYINT         NOT NULL,
        [EditorPassword]      NVARCHAR(255)   NOT NULL,
        [EditorOrcid]         NVARCHAR(200)   NULL,
        [EditorGoogleScholar] NVARCHAR(250)   NULL,
        [EditorWebsite]       NVARCHAR(250)   NULL,
        [EditorCreatedDate]   DATETIME2       NOT NULL CONSTRAINT [DF_Editors_EditorCreatedDate] DEFAULT (GETUTCDATE()),
        [EditorUpdatedDate]   DATETIME2       NULL,

        CONSTRAINT [PK_Editors] PRIMARY KEY CLUSTERED ([EditorId] ASC)
    );
    PRINT 'Table Editors created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Editors_EditorEmail' AND object_id = OBJECT_ID(N'[dbo].[Editors]'))
    CREATE NONCLUSTERED INDEX [IX_Editors_EditorEmail] ON [dbo].[Editors] ([EditorEmail] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Editors_EditorStatus' AND object_id = OBJECT_ID(N'[dbo].[Editors]'))
    CREATE NONCLUSTERED INDEX [IX_Editors_EditorStatus] ON [dbo].[Editors] ([EditorStatus] ASC);
GO

-- ---------------------------------------------------------------------------
-- EditorProfiles
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[EditorProfiles]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[EditorProfiles]
    (
        [EditorId]          INT             NOT NULL IDENTITY(1,1),
        [EditorName]        NVARCHAR(100)   NOT NULL,
        [EditorEmail]       NVARCHAR(150)   NOT NULL,
        [EditorPhone]       NVARCHAR(20)    NULL,
        [EditorDesignation] NVARCHAR(100)   NULL,
        [EditorImage]       NVARCHAR(255)   NULL,
        [EditorDescription] NVARCHAR(MAX)   NULL,
        -- 0 = Inactive, 1 = Active
        [EditorStatus]      TINYINT         NOT NULL,
        [EditorPassword]    NVARCHAR(255)   NOT NULL,
        [EditorCreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_EditorProfiles_EditorCreatedDate] DEFAULT (GETUTCDATE()),
        [EditorUpdatedDate] DATETIME2       NULL,

        CONSTRAINT [PK_EditorProfiles] PRIMARY KEY CLUSTERED ([EditorId] ASC)
    );
    PRINT 'Table EditorProfiles created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_EditorProfiles_EditorEmail' AND object_id = OBJECT_ID(N'[dbo].[EditorProfiles]'))
    CREATE NONCLUSTERED INDEX [IX_EditorProfiles_EditorEmail] ON [dbo].[EditorProfiles] ([EditorEmail] ASC);
GO

-- ---------------------------------------------------------------------------
-- Reviewers
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Reviewers]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Reviewers]
    (
        [ReviewerId]          INT             NOT NULL IDENTITY(1,1),
        [ReviewerName]        NVARCHAR(100)   NOT NULL,
        [ReviewerEmail]       NVARCHAR(150)   NOT NULL,
        [ReviewerPhone]       NVARCHAR(20)    NULL,
        [ReviewerCountry]     NVARCHAR(50)    NULL,
        [ReviewerDesignation] NVARCHAR(100)   NULL,
        [ReviewerImage]       NVARCHAR(255)   NULL,
        [ReviewerAddress]     NVARCHAR(MAX)   NULL,
        [ReviewerDescription] NVARCHAR(MAX)   NULL,
        -- 0 = Inactive, 1 = Active
        [ReviewerStatus]      TINYINT         NOT NULL,
        [ReviewerPassword]    NVARCHAR(255)   NOT NULL,
        [ReviewerCreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Reviewers_ReviewerCreatedDate] DEFAULT (GETUTCDATE()),
        [ReviewerUpdatedDate] DATETIME2       NULL,

        CONSTRAINT [PK_Reviewers] PRIMARY KEY CLUSTERED ([ReviewerId] ASC)
    );
    PRINT 'Table Reviewers created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Reviewers_ReviewerEmail' AND object_id = OBJECT_ID(N'[dbo].[Reviewers]'))
    CREATE NONCLUSTERED INDEX [IX_Reviewers_ReviewerEmail] ON [dbo].[Reviewers] ([ReviewerEmail] ASC);
GO

-- ---------------------------------------------------------------------------
-- Authors
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Authors]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Authors]
    (
        [AuthorId]          INT             NOT NULL IDENTITY(1,1),
        [AuthorName]        NVARCHAR(100)   NOT NULL,
        [AuthorEmail]       NVARCHAR(150)   NOT NULL,
        [AuthorPhone]       NVARCHAR(20)    NULL,
        [AuthorCountry]     NVARCHAR(100)   NULL,
        [AuthorDesignation] NVARCHAR(100)   NULL,
        [AuthorDescription] NVARCHAR(MAX)   NULL,
        [AuthorImage]       NVARCHAR(255)   NULL,
        [AuthorAddress]     NVARCHAR(MAX)   NULL,
        -- 0 = Inactive, 1 = Active
        [AuthorStatus]      TINYINT         NOT NULL,
        [AuthorPassword]    NVARCHAR(255)   NULL,
        [AuthorCreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Authors_AuthorCreatedDate] DEFAULT (GETUTCDATE()),
        [AuthorUpdatedDate] DATETIME2       NULL,

        CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([AuthorId] ASC)
    );
    PRINT 'Table Authors created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Authors_AuthorEmail' AND object_id = OBJECT_ID(N'[dbo].[Authors]'))
    CREATE NONCLUSTERED INDEX [IX_Authors_AuthorEmail] ON [dbo].[Authors] ([AuthorEmail] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Authors_AuthorStatus' AND object_id = OBJECT_ID(N'[dbo].[Authors]'))
    CREATE NONCLUSTERED INDEX [IX_Authors_AuthorStatus] ON [dbo].[Authors] ([AuthorStatus] ASC);
GO

-- ---------------------------------------------------------------------------
-- Banners
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Banners]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Banners]
    (
        [BannerId]          INT             NOT NULL IDENTITY(1,1),
        [BannerHeading]     NVARCHAR(100)   NOT NULL,
        [BannerDescription] NVARCHAR(MAX)   NOT NULL,
        [BannerImage]       NVARCHAR(200)   NULL,
        -- 0 = Inactive, 1 = Active
        [BannerStatus]      TINYINT         NOT NULL CONSTRAINT [DF_Banners_BannerStatus] DEFAULT (1),
        [CreatedDate]       DATETIME2       NOT NULL CONSTRAINT [DF_Banners_CreatedDate]   DEFAULT (GETUTCDATE()),
        [UpdatedDate]       DATETIME2       NOT NULL,

        CONSTRAINT [PK_Banners] PRIMARY KEY CLUSTERED ([BannerId] ASC)
    );
    PRINT 'Table Banners created.';
END
GO

-- ---------------------------------------------------------------------------
-- Contacts
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Contacts]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Contacts]
    (
        [ContactId]      INT             NOT NULL IDENTITY(1,1),
        [ContactName]    NVARCHAR(100)   NOT NULL,
        [ContactEmail]   NVARCHAR(255)   NOT NULL,
        [ContactPhone]   NVARCHAR(20)    NOT NULL,
        [ContactSubject] NVARCHAR(100)   NOT NULL,
        [ContactMessage] NVARCHAR(MAX)   NOT NULL,
        [ContactStatus]  NVARCHAR(20)    NOT NULL
            CONSTRAINT [DF_Contacts_ContactStatus] DEFAULT (N'pending')
            CONSTRAINT [CK_Contacts_ContactStatus] CHECK (
                [ContactStatus] IN (N'pending', N'contacted', N'cancelled')
            ),
        [CreatedDate]    DATETIME2       NOT NULL CONSTRAINT [DF_Contacts_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate]    DATETIME2       NOT NULL,

        CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ContactId] ASC)
    );
    PRINT 'Table Contacts created.';
END
GO

-- ---------------------------------------------------------------------------
-- Testimonials
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Testimonials]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Testimonials]
    (
        [TestimonialId]          INT             NOT NULL IDENTITY(1,1),
        [TestimonialName]        NVARCHAR(100)   NOT NULL,
        [TestimonialImage]       NVARCHAR(100)   NOT NULL,
        [TestimonialDescription] NVARCHAR(MAX)   NOT NULL,
        -- 0 = Inactive, 1 = Active
        [TestimonialStatus]      TINYINT         NOT NULL,
        [TestimonialCreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Testimonials_TestimonialCreatedDate] DEFAULT (GETUTCDATE()),
        [TestimonialUpdatedDate] DATETIME2       NULL,

        CONSTRAINT [PK_Testimonials] PRIMARY KEY CLUSTERED ([TestimonialId] ASC)
    );
    PRINT 'Table Testimonials created.';
END
GO

-- ---------------------------------------------------------------------------
-- Collaborators
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Collaborators]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Collaborators]
    (
        [CollaboratorId]     INT             NOT NULL IDENTITY(1,1),
        [CollaboratorName]   NVARCHAR(100)   NOT NULL,
        [CollaboratorUrl]    NVARCHAR(MAX)   NOT NULL,
        [CollaboratorImage]  NVARCHAR(200)   NULL,
        -- 0 = Inactive, 1 = Active
        [CollaboratorStatus] TINYINT         NOT NULL CONSTRAINT [DF_Collaborators_CollaboratorStatus] DEFAULT (1),
        [CreatedDate]        DATETIME2       NOT NULL CONSTRAINT [DF_Collaborators_CreatedDate]        DEFAULT (GETUTCDATE()),
        [UpdatedDate]        DATETIME2       NULL,

        CONSTRAINT [PK_Collaborators] PRIMARY KEY CLUSTERED ([CollaboratorId] ASC)
    );
    PRINT 'Table Collaborators created.';
END
GO

-- ---------------------------------------------------------------------------
-- Pages
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Pages]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Pages]
    (
        [PageId]      INT             NOT NULL IDENTITY(1,1),
        [PageSlug]    NVARCHAR(250)   NOT NULL,
        [PageTitle]   NVARCHAR(MAX)   NOT NULL,
        [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Pages_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate] DATETIME2       NOT NULL,

        CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED ([PageId] ASC)
    );
    PRINT 'Table Pages created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Pages_PageSlug' AND object_id = OBJECT_ID(N'[dbo].[Pages]'))
    CREATE NONCLUSTERED INDEX [IX_Pages_PageSlug] ON [dbo].[Pages] ([PageSlug] ASC);
GO

-- ---------------------------------------------------------------------------
-- Biography
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Biography]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Biography]
    (
        [Id]          INT             NOT NULL IDENTITY(1,1),
        -- References Editors.EditorId
        [ProfeId]     INT             NOT NULL,
        [Biography]   NVARCHAR(MAX)   NOT NULL,
        [Status]      NVARCHAR(100)   NOT NULL CONSTRAINT [DF_Biography_Status]      DEFAULT (N'Inactive'),
        [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Biography_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate] DATETIME2       NOT NULL,

        CONSTRAINT [PK_Biography] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table Biography created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Biography_ProfeId' AND object_id = OBJECT_ID(N'[dbo].[Biography]'))
    CREATE NONCLUSTERED INDEX [IX_Biography_ProfeId] ON [dbo].[Biography] ([ProfeId] ASC);
GO

-- ---------------------------------------------------------------------------
-- OldArticles
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[OldArticles]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[OldArticles]
    (
        [Id]             INT             NOT NULL IDENTITY(1,1),
        [ArticleName]    NVARCHAR(255)   NOT NULL,
        [ArticleSeoName] NVARCHAR(255)   NOT NULL,
        [ArticleId]      INT             NOT NULL,
        [Type]           INT             NOT NULL,
        [CreatedDate]    DATETIME2       NOT NULL CONSTRAINT [DF_OldArticles_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate]    DATETIME2       NOT NULL,

        CONSTRAINT [PK_OldArticles] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table OldArticles created.';
END
GO

-- ---------------------------------------------------------------------------
-- SeoArticles
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[SeoArticles]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SeoArticles]
    (
        [Id]          BIGINT          NOT NULL IDENTITY(1,1),
        [ArticleName] NVARCHAR(MAX)   NOT NULL,
        [Type]        NVARCHAR(255)   NOT NULL,
        [ArticleId]   INT             NOT NULL,

        CONSTRAINT [PK_SeoArticles] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table SeoArticles created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_SeoArticles_ArticleId' AND object_id = OBJECT_ID(N'[dbo].[SeoArticles]'))
    CREATE NONCLUSTERED INDEX [IX_SeoArticles_ArticleId] ON [dbo].[SeoArticles] ([ArticleId] ASC);
GO

-- ---------------------------------------------------------------------------
-- MemberRequestReviewer
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[MemberRequestReviewer]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[MemberRequestReviewer]
    (
        [Id]          BIGINT          NOT NULL IDENTITY(1,1),
        [Name]        NVARCHAR(255)   NOT NULL,
        [Email]       NVARCHAR(255)   NOT NULL,
        [PhoneNumber] NVARCHAR(20)    NOT NULL,
        [Designation] NVARCHAR(255)   NOT NULL,
        [Country]     NVARCHAR(255)   NOT NULL,
        [BioData]     NVARCHAR(MAX)   NOT NULL,
        [Files]       NVARCHAR(255)   NOT NULL,
        [Type]        INT             NOT NULL,
        [JournalId]   INT             NOT NULL,
        [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_MemberRequestReviewer_CreatedDate] DEFAULT (GETUTCDATE()),

        CONSTRAINT [PK_MemberRequestReviewer] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table MemberRequestReviewer created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_MemberRequestReviewer_JournalId' AND object_id = OBJECT_ID(N'[dbo].[MemberRequestReviewer]'))
    CREATE NONCLUSTERED INDEX [IX_MemberRequestReviewer_JournalId] ON [dbo].[MemberRequestReviewer] ([JournalId] ASC);
GO

-- ---------------------------------------------------------------------------
-- TempCkeditorImages
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[TempCkeditorImages]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TempCkeditorImages]
    (
        [TempCkeditorImageId] INT             NOT NULL IDENTITY(1,1),
        [ArticleId]           INT             NULL,
        [DataOf]              NVARCHAR(100)   NULL,
        [FileName]            NVARCHAR(255)   NOT NULL,
        [FileUrl]             NVARCHAR(500)   NOT NULL,
        [FileSaved]           NVARCHAR(3)     NOT NULL
            CONSTRAINT [DF_TempCkeditorImages_FileSaved] DEFAULT (N'no')
            CONSTRAINT [CK_TempCkeditorImages_FileSaved] CHECK ([FileSaved] IN (N'yes', N'no')),
        [CreatedDate]         DATETIME2       NOT NULL CONSTRAINT [DF_TempCkeditorImages_CreatedDate] DEFAULT (GETUTCDATE()),

        CONSTRAINT [PK_TempCkeditorImages] PRIMARY KEY CLUSTERED ([TempCkeditorImageId] ASC)
    );
    PRINT 'Table TempCkeditorImages created.';
END
GO

-- ---------------------------------------------------------------------------
-- TempExternalImages
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[TempExternalImages]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TempExternalImages]
    (
        [TempExternalImageId] INT             NOT NULL IDENTITY(1,1),
        [ArticleId]           INT             NULL,
        [DataOf]              NVARCHAR(100)   NULL,
        [FileName]            NVARCHAR(255)   NOT NULL,
        [FileUrl]             NVARCHAR(500)   NOT NULL,
        [FileSaved]           NVARCHAR(3)     NOT NULL
            CONSTRAINT [DF_TempExternalImages_FileSaved] DEFAULT (N'no')
            CONSTRAINT [CK_TempExternalImages_FileSaved] CHECK ([FileSaved] IN (N'yes', N'no')),
        [CreatedDate]         DATETIME2       NOT NULL CONSTRAINT [DF_TempExternalImages_CreatedDate] DEFAULT (GETUTCDATE()),

        CONSTRAINT [PK_TempExternalImages] PRIMARY KEY CLUSTERED ([TempExternalImageId] ASC)
    );
    PRINT 'Table TempExternalImages created.';
END
GO

-- ---------------------------------------------------------------------------
-- SubscriptionList
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[SubscriptionList]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SubscriptionList]
    (
        [SubscriptionId]     INT             NOT NULL IDENTITY(1,1),
        [EmailId]            NVARCHAR(255)   NOT NULL,
        [SubscribedFor]      NVARCHAR(150)   NULL,
        [SubscribedForId]    INT             NOT NULL,
        -- 0 = Unsubscribed, 1 = Subscribed
        [SubscriptionStatus] TINYINT         NOT NULL CONSTRAINT [DF_SubscriptionList_SubscriptionStatus] DEFAULT (1),
        [CreatedDate]        DATETIME2       NOT NULL CONSTRAINT [DF_SubscriptionList_CreatedDate]        DEFAULT (GETUTCDATE()),
        [UpdatedDate]        DATETIME2       NOT NULL,

        CONSTRAINT [PK_SubscriptionList] PRIMARY KEY CLUSTERED ([SubscriptionId] ASC)
    );
    PRINT 'Table SubscriptionList created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_SubscriptionList_EmailId' AND object_id = OBJECT_ID(N'[dbo].[SubscriptionList]'))
    CREATE NONCLUSTERED INDEX [IX_SubscriptionList_EmailId] ON [dbo].[SubscriptionList] ([EmailId] ASC);
GO

-- ---------------------------------------------------------------------------
-- SpecialIssueRequest
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[SpecialIssueRequest]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SpecialIssueRequest]
    (
        [RequestId]   INT             NOT NULL IDENTITY(1,1),
        [JournalId]   NVARCHAR(200)   NOT NULL,
        [ArticleType] NVARCHAR(100)   NOT NULL,
        [Title]       NVARCHAR(200)   NOT NULL,
        [Name]        NVARCHAR(100)   NOT NULL,
        [Country]     NVARCHAR(50)    NOT NULL,
        [EmailId]     NVARCHAR(50)    NOT NULL,
        [PhoneNumber] NVARCHAR(20)    NOT NULL,
        [Address]     NVARCHAR(MAX)   NOT NULL,
        [FileLink]    NVARCHAR(100)   NOT NULL,
        [Description] NVARCHAR(MAX)   NOT NULL,
        [Status]      NVARCHAR(50)    NOT NULL,
        [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_SpecialIssueRequest_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate] DATETIME2       NOT NULL,

        CONSTRAINT [PK_SpecialIssueRequest] PRIMARY KEY CLUSTERED ([RequestId] ASC)
    );
    PRINT 'Table SpecialIssueRequest created.';
END
GO

-- ---------------------------------------------------------------------------
-- Articles  (FK → Journals)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Articles]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Articles]
    (
        [ArticleId]                 BIGINT          NOT NULL IDENTITY(1,1),
        [ArticleViews]              INT             NOT NULL CONSTRAINT [DF_Articles_ArticleViews]     DEFAULT (1),
        [ArticleDownloads]          INT             NOT NULL CONSTRAINT [DF_Articles_ArticleDownloads] DEFAULT (1),
        -- Descriptive label e.g. 'Review Article', 'Case Report'
        [ArticleFor]                NVARCHAR(100)   NOT NULL,
        [JournalId]                 INT             NOT NULL,
        [VolumeNo]                  INT             NOT NULL,
        [IssueNo]                   INT             NOT NULL,
        -- 0 = Article In Press, 1 = Current Issue, 2 = Archive
        [ArticleType]               TINYINT         NOT NULL,
        [ArticleName]               NVARCHAR(500)   NOT NULL,
        [ArticleSeoName]            NVARCHAR(500)   NOT NULL,
        [ArticleDoi]                NVARCHAR(100)   NULL,
        [CorrespondingAuthor]       NVARCHAR(MAX)   NOT NULL,
        [ReceivedDate]              DATE            NULL,
        [AcceptedDate]              DATE            NULL,
        [PublishedDate]             DATE            NULL,
        [Citation]                  NVARCHAR(1000)  NULL,
        [Copyright]                 NVARCHAR(1000)  NULL,
        [ArticleInformation]        NVARCHAR(MAX)   NULL,
        [ArticleAbstract]           NVARCHAR(MAX)   NULL,
        [DisplayArticleInformation] NVARCHAR(MAX)   NULL,
        [DisplayArticleAbstract]    NVARCHAR(MAX)   NULL,
        [AddressDesc]               NVARCHAR(MAX)   NULL,
        [AbstractKeywords]          NVARCHAR(500)   NULL,
        [ArticlePdf]                NVARCHAR(255)   NOT NULL,
        -- 0 = Inactive, 1 = Active
        [ArticleStatus]             TINYINT         NOT NULL CONSTRAINT [DF_Articles_ArticleStatus]      DEFAULT (1),
        [ArticleCreatedDate]        DATETIME2       NOT NULL CONSTRAINT [DF_Articles_ArticleCreatedDate] DEFAULT (GETUTCDATE()),
        [ArticleUpdatedDate]        DATETIME2       NULL,

        CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED ([ArticleId] ASC),
        CONSTRAINT [FK_Articles_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId])
    );
    PRINT 'Table Articles created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Articles_JournalId' AND object_id = OBJECT_ID(N'[dbo].[Articles]'))
    CREATE NONCLUSTERED INDEX [IX_Articles_JournalId] ON [dbo].[Articles] ([JournalId] ASC)
        INCLUDE ([ArticleType], [ArticleStatus], [PublishedDate], [VolumeNo], [IssueNo]);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Articles_JournalVolumeIssue' AND object_id = OBJECT_ID(N'[dbo].[Articles]'))
    CREATE NONCLUSTERED INDEX [IX_Articles_JournalVolumeIssue] ON [dbo].[Articles] ([JournalId] ASC, [VolumeNo] ASC, [IssueNo] ASC)
        INCLUDE ([ArticleType], [ArticleStatus]);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Articles_ArticleType_Status' AND object_id = OBJECT_ID(N'[dbo].[Articles]'))
    CREATE NONCLUSTERED INDEX [IX_Articles_ArticleType_Status] ON [dbo].[Articles] ([ArticleType] ASC, [ArticleStatus] ASC)
        INCLUDE ([JournalId], [PublishedDate]);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Articles_PublishedDate' AND object_id = OBJECT_ID(N'[dbo].[Articles]'))
    CREATE NONCLUSTERED INDEX [IX_Articles_PublishedDate] ON [dbo].[Articles] ([PublishedDate] DESC)
        INCLUDE ([JournalId], [ArticleStatus]);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Articles_ArticleSeoName' AND object_id = OBJECT_ID(N'[dbo].[Articles]'))
    CREATE NONCLUSTERED INDEX [IX_Articles_ArticleSeoName] ON [dbo].[Articles] ([ArticleSeoName] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Articles_ArticleDoi' AND object_id = OBJECT_ID(N'[dbo].[Articles]'))
    CREATE NONCLUSTERED INDEX [IX_Articles_ArticleDoi] ON [dbo].[Articles] ([ArticleDoi] ASC)
        WHERE [ArticleDoi] IS NOT NULL;
GO

-- ---------------------------------------------------------------------------
-- AbstractingIndexing  (FK → Journals)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[AbstractingIndexing]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AbstractingIndexing]
    (
        [AbstractionId]    BIGINT          NOT NULL IDENTITY(1,1),
        [JournalId]        INT             NOT NULL,
        [AbstractionUrl]   NVARCHAR(MAX)   NOT NULL,
        [AbstractionTitle] NVARCHAR(255)   NOT NULL,
        [Status]           TINYINT         NOT NULL CONSTRAINT [DF_AbstractingIndexing_Status]      DEFAULT (1),
        [CreatedDate]      DATETIME2       NOT NULL CONSTRAINT [DF_AbstractingIndexing_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate]      DATETIME2       NULL,

        CONSTRAINT [PK_AbstractingIndexing] PRIMARY KEY CLUSTERED ([AbstractionId] ASC),
        CONSTRAINT [FK_AbstractingIndexing_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId])
    );
    PRINT 'Table AbstractingIndexing created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_AbstractingIndexing_JournalId' AND object_id = OBJECT_ID(N'[dbo].[AbstractingIndexing]'))
    CREATE NONCLUSTERED INDEX [IX_AbstractingIndexing_JournalId] ON [dbo].[AbstractingIndexing] ([JournalId] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_AbstractingIndexing_Status' AND object_id = OBJECT_ID(N'[dbo].[AbstractingIndexing]'))
    CREATE NONCLUSTERED INDEX [IX_AbstractingIndexing_Status] ON [dbo].[AbstractingIndexing] ([Status] ASC);
GO

-- ---------------------------------------------------------------------------
-- ArticleProcessingCharges  (FK → Journals)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[ArticleProcessingCharges]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ArticleProcessingCharges]
    (
        [ApcId]       BIGINT      NOT NULL IDENTITY(1,1),
        [JournalId]   INT         NOT NULL,
        [ApcAmount]   INT         NOT NULL,
        [CreatedDate] DATETIME2   NOT NULL CONSTRAINT [DF_ArticleProcessingCharges_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate] DATETIME2   NULL,

        CONSTRAINT [PK_ArticleProcessingCharges] PRIMARY KEY CLUSTERED ([ApcId] ASC),
        CONSTRAINT [FK_ArticleProcessingCharges_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId])
    );
    PRINT 'Table ArticleProcessingCharges created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ArticleProcessingCharges_JournalId' AND object_id = OBJECT_ID(N'[dbo].[ArticleProcessingCharges]'))
    CREATE NONCLUSTERED INDEX [IX_ArticleProcessingCharges_JournalId] ON [dbo].[ArticleProcessingCharges] ([JournalId] ASC);
GO

-- ---------------------------------------------------------------------------
-- JournalEditorialBoard  (FK → Journals)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[JournalEditorialBoard]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[JournalEditorialBoard]
    (
        [EditorialBoardId] INT             NOT NULL IDENTITY(1,1),
        [JournalId]        INT             NOT NULL,
        -- References Editors.EditorId or EditorProfiles.EditorId depending on EditorRole
        [EditorId]         INT             NOT NULL,
        [EditorRole]       NVARCHAR(30)    NULL
            CONSTRAINT [CK_JournalEditorialBoard_EditorRole] CHECK (
                [EditorRole] IN (N'Manuscript Editor', N'Assistant Editor',
                                 N'Consulting Editor', N'Associate Editor', N'Editor Profiles')
            ),

        CONSTRAINT [PK_JournalEditorialBoard] PRIMARY KEY CLUSTERED ([EditorialBoardId] ASC),
        CONSTRAINT [FK_JournalEditorialBoard_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId])
    );
    PRINT 'Table JournalEditorialBoard created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_JournalEditorialBoard_JournalId' AND object_id = OBJECT_ID(N'[dbo].[JournalEditorialBoard]'))
    CREATE NONCLUSTERED INDEX [IX_JournalEditorialBoard_JournalId] ON [dbo].[JournalEditorialBoard] ([JournalId] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_JournalEditorialBoard_EditorId' AND object_id = OBJECT_ID(N'[dbo].[JournalEditorialBoard]'))
    CREATE NONCLUSTERED INDEX [IX_JournalEditorialBoard_EditorId] ON [dbo].[JournalEditorialBoard] ([EditorId] ASC);
GO

-- ---------------------------------------------------------------------------
-- JournalSections  (FK → Journals)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[JournalSections]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[JournalSections]
    (
        [SectionId]   INT             NOT NULL IDENTITY(1,1),
        [JournalId]   INT             NOT NULL,
        [SectionName] NVARCHAR(100)   NOT NULL,

        CONSTRAINT [PK_JournalSections] PRIMARY KEY CLUSTERED ([SectionId] ASC),
        CONSTRAINT [FK_JournalSections_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId])
    );
    PRINT 'Table JournalSections created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_JournalSections_JournalId' AND object_id = OBJECT_ID(N'[dbo].[JournalSections]'))
    CREATE NONCLUSTERED INDEX [IX_JournalSections_JournalId] ON [dbo].[JournalSections] ([JournalId] ASC);
GO

-- ---------------------------------------------------------------------------
-- MemberedIn  (FK → Journals)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[MemberedIn]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[MemberedIn]
    (
        [Id]          INT             NOT NULL IDENTITY(1,1),
        [JournalId]   INT             NOT NULL,
        [Name]        NVARCHAR(200)   NOT NULL,
        [Image]       NVARCHAR(255)   NOT NULL,
        [Url]         NVARCHAR(250)   NOT NULL,
        [Status]      INT             NOT NULL CONSTRAINT [DF_MemberedIn_Status]      DEFAULT (1),
        [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_MemberedIn_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate] DATETIME2       NOT NULL,

        CONSTRAINT [PK_MemberedIn] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_MemberedIn_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId])
    );
    PRINT 'Table MemberedIn created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_MemberedIn_JournalId' AND object_id = OBJECT_ID(N'[dbo].[MemberedIn]'))
    CREATE NONCLUSTERED INDEX [IX_MemberedIn_JournalId] ON [dbo].[MemberedIn] ([JournalId] ASC);
GO

-- ---------------------------------------------------------------------------
-- PubmedIndex  (FK → Journals)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[PubmedIndex]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PubmedIndex]
    (
        [PubmedIndexId] INT             NOT NULL IDENTITY(1,1),
        [JournalId]     INT             NOT NULL,
        [IndexText]     NVARCHAR(MAX)   NOT NULL,
        [PubmedUrl]     NVARCHAR(255)   NOT NULL,
        [PmcUrl]        NVARCHAR(255)   NOT NULL,
        [CreatedDate]   DATETIME2       NOT NULL CONSTRAINT [DF_PubmedIndex_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate]   DATETIME2       NOT NULL,

        CONSTRAINT [PK_PubmedIndex] PRIMARY KEY CLUSTERED ([PubmedIndexId] ASC),
        CONSTRAINT [FK_PubmedIndex_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId])
    );
    PRINT 'Table PubmedIndex created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_PubmedIndex_JournalId' AND object_id = OBJECT_ID(N'[dbo].[PubmedIndex]'))
    CREATE NONCLUSTERED INDEX [IX_PubmedIndex_JournalId] ON [dbo].[PubmedIndex] ([JournalId] ASC);
GO

-- ---------------------------------------------------------------------------
-- SpecialIssues
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[SpecialIssues]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SpecialIssues]
    (
        [IssueId]          BIGINT          NOT NULL IDENTITY(1,1),
        [Title]            NVARCHAR(MAX)   NOT NULL,
        [JournalId]        BIGINT          NOT NULL,
        [Slug]             NVARCHAR(200)   NOT NULL,
        [DeadlineDate]     DATE            NOT NULL,
        [ViewCount]        INT             NOT NULL,
        [IssnNumber]       NVARCHAR(100)   NOT NULL,
        [CitiesScore]      NVARCHAR(50)    NOT NULL,
        [ImpactFactor]     NVARCHAR(50)    NOT NULL,
        [FileLink]         NVARCHAR(50)    NOT NULL,
        [IssueData]        NVARCHAR(MAX)   NOT NULL,
        [IssueDataDisplay] NVARCHAR(MAX)   NOT NULL,
        [Keywords]         NVARCHAR(MAX)   NOT NULL,
        [Benefits]         NVARCHAR(MAX)   NOT NULL,
        [CreatedDate]      DATETIME2       NOT NULL CONSTRAINT [DF_SpecialIssues_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate]      DATETIME2       NOT NULL,
        [Status]           INT             NOT NULL,

        CONSTRAINT [PK_SpecialIssues] PRIMARY KEY CLUSTERED ([IssueId] ASC)
    );
    PRINT 'Table SpecialIssues created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_SpecialIssues_JournalId' AND object_id = OBJECT_ID(N'[dbo].[SpecialIssues]'))
    CREATE NONCLUSTERED INDEX [IX_SpecialIssues_JournalId] ON [dbo].[SpecialIssues] ([JournalId] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_SpecialIssues_Slug' AND object_id = OBJECT_ID(N'[dbo].[SpecialIssues]'))
    CREATE NONCLUSTERED INDEX [IX_SpecialIssues_Slug] ON [dbo].[SpecialIssues] ([Slug] ASC);
GO

-- ---------------------------------------------------------------------------
-- Documents
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Documents]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Documents]
    (
        [DocId]       BIGINT          NOT NULL IDENTITY(1,1),
        [Title]       NVARCHAR(MAX)   NOT NULL,
        [PageSlug]    NVARCHAR(255)   NOT NULL,
        [DocData]     NVARCHAR(MAX)   NOT NULL,
        [JournalId]   BIGINT          NULL,
        [SeoKeywords] NVARCHAR(MAX)   NOT NULL,
        [Type]        NVARCHAR(100)   NULL,
        [Status]      INT             NOT NULL CONSTRAINT [DF_Documents_Status]      DEFAULT (1),
        [CreatedBy]   INT             NULL,
        [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Documents_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate] DATETIME2       NOT NULL,

        CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([DocId] ASC)
    );
    PRINT 'Table Documents created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Documents_JournalId' AND object_id = OBJECT_ID(N'[dbo].[Documents]'))
    CREATE NONCLUSTERED INDEX [IX_Documents_JournalId] ON [dbo].[Documents] ([JournalId] ASC)
        WHERE [JournalId] IS NOT NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Documents_PageSlug' AND object_id = OBJECT_ID(N'[dbo].[Documents]'))
    CREATE NONCLUSTERED INDEX [IX_Documents_PageSlug] ON [dbo].[Documents] ([PageSlug] ASC);
GO

-- ---------------------------------------------------------------------------
-- ManuscriptRequest
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[ManuscriptRequest]', N'U') IS NULL
BEGIN
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
        -- 1 = Requested, 2 = Reached, 3 = Approved, 4 = Published
        [Status]          INT             NOT NULL CONSTRAINT [DF_ManuscriptRequest_Status] DEFAULT (1),
        [CreatedDate]     DATETIME2       NOT NULL CONSTRAINT [DF_ManuscriptRequest_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate]     DATETIME2       NOT NULL,

        CONSTRAINT [PK_ManuscriptRequest] PRIMARY KEY CLUSTERED ([RequestId] ASC)
    );
    PRINT 'Table ManuscriptRequest created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ManuscriptRequest_JournalId' AND object_id = OBJECT_ID(N'[dbo].[ManuscriptRequest]'))
    CREATE NONCLUSTERED INDEX [IX_ManuscriptRequest_JournalId] ON [dbo].[ManuscriptRequest] ([JournalId] ASC)
        WHERE [JournalId] IS NOT NULL;
GO

-- ---------------------------------------------------------------------------
-- ArticleAuthors  (FK → Articles)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[ArticleAuthors]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ArticleAuthors]
    (
        [ArticleAuthorId] BIGINT          NOT NULL IDENTITY(1,1),
        [ArticleId]       BIGINT          NOT NULL,
        [AuthorName]      NVARCHAR(255)   NOT NULL,
        [AuthorNo]        TINYINT         NOT NULL,
        [AuthorSubhead]   NVARCHAR(5)     NOT NULL,
        [AuthorEmail]     NVARCHAR(255)   NULL,
        [AuthorOrcid]     NVARCHAR(255)   NULL,
        [AuthorAddress]   NVARCHAR(MAX)   NULL,

        CONSTRAINT [PK_ArticleAuthors] PRIMARY KEY CLUSTERED ([ArticleAuthorId] ASC),
        CONSTRAINT [FK_ArticleAuthors_Articles] FOREIGN KEY ([ArticleId])
            REFERENCES [dbo].[Articles] ([ArticleId])
    );
    PRINT 'Table ArticleAuthors created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ArticleAuthors_ArticleId' AND object_id = OBJECT_ID(N'[dbo].[ArticleAuthors]'))
    CREATE NONCLUSTERED INDEX [IX_ArticleAuthors_ArticleId] ON [dbo].[ArticleAuthors] ([ArticleId] ASC);
GO

-- ---------------------------------------------------------------------------
-- ArticleDetails  (FK → Articles)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[ArticleDetails]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ArticleDetails]
    (
        [DetailsId]                 BIGINT          NOT NULL IDENTITY(1,1),
        [DetailsArticleId]          BIGINT          NOT NULL,
        [DetailsHeading]            NVARCHAR(100)   NOT NULL,
        [DetailsDescription]        NVARCHAR(MAX)   NULL,
        [DisplayDetailsDescription] NVARCHAR(MAX)   NULL,
        [CreatedDate]               DATETIME2       NOT NULL CONSTRAINT [DF_ArticleDetails_CreatedDate] DEFAULT (GETUTCDATE()),

        CONSTRAINT [PK_ArticleDetails] PRIMARY KEY CLUSTERED ([DetailsId] ASC),
        CONSTRAINT [FK_ArticleDetails_Articles] FOREIGN KEY ([DetailsArticleId])
            REFERENCES [dbo].[Articles] ([ArticleId])
    );
    PRINT 'Table ArticleDetails created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ArticleDetails_DetailsArticleId' AND object_id = OBJECT_ID(N'[dbo].[ArticleDetails]'))
    CREATE NONCLUSTERED INDEX [IX_ArticleDetails_DetailsArticleId] ON [dbo].[ArticleDetails] ([DetailsArticleId] ASC);
GO

-- ---------------------------------------------------------------------------
-- ArticleReferences  (FK → Articles)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[ArticleReferences]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ArticleReferences]
    (
        [ReferenceId]            BIGINT          NOT NULL IDENTITY(1,1),
        [ReferenceArticleId]     BIGINT          NOT NULL,
        [ReferenceData]          NVARCHAR(MAX)   NOT NULL,
        [ReferenceGoogleLink]    NVARCHAR(MAX)   NOT NULL,
        [ReferencePublisherLink] NVARCHAR(MAX)   NOT NULL,

        CONSTRAINT [PK_ArticleReferences] PRIMARY KEY CLUSTERED ([ReferenceId] ASC),
        CONSTRAINT [FK_ArticleReferences_Articles] FOREIGN KEY ([ReferenceArticleId])
            REFERENCES [dbo].[Articles] ([ArticleId])
    );
    PRINT 'Table ArticleReferences created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ArticleReferences_ReferenceArticleId' AND object_id = OBJECT_ID(N'[dbo].[ArticleReferences]'))
    CREATE NONCLUSTERED INDEX [IX_ArticleReferences_ReferenceArticleId] ON [dbo].[ArticleReferences] ([ReferenceArticleId] ASC);
GO

-- ---------------------------------------------------------------------------
-- Manuscripts  (FK → Journals, Authors)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[Manuscripts]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Manuscripts]
    (
        [ManuscriptId]          INT             NOT NULL IDENTITY(1,1),
        [JournalId]             INT             NOT NULL,
        [ManuscriptSubmittedBy] INT             NOT NULL,
        [ManuscriptTitle]       NVARCHAR(150)   NOT NULL,
        [ManuscriptType]        NVARCHAR(50)    NOT NULL,
        [MessageToEditor]       NVARCHAR(MAX)   NOT NULL,
        [ManuscriptFile]        NVARCHAR(150)   NOT NULL,
        -- 0 = Pending, 1 = Accepted, 2 = Published, 3 = Rejected
        [ManuscriptStatus]      TINYINT         NOT NULL,
        [ManuscriptCreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Manuscripts_ManuscriptCreatedDate] DEFAULT (GETUTCDATE()),
        [ManuscriptUpdatedDate] DATETIME2       NULL,

        CONSTRAINT [PK_Manuscripts] PRIMARY KEY CLUSTERED ([ManuscriptId] ASC),
        CONSTRAINT [FK_Manuscripts_Journals] FOREIGN KEY ([JournalId])
            REFERENCES [dbo].[Journals] ([JournalId]),
        CONSTRAINT [FK_Manuscripts_Authors] FOREIGN KEY ([ManuscriptSubmittedBy])
            REFERENCES [dbo].[Authors] ([AuthorId])
    );
    PRINT 'Table Manuscripts created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Manuscripts_JournalId' AND object_id = OBJECT_ID(N'[dbo].[Manuscripts]'))
    CREATE NONCLUSTERED INDEX [IX_Manuscripts_JournalId] ON [dbo].[Manuscripts] ([JournalId] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Manuscripts_ManuscriptSubmittedBy' AND object_id = OBJECT_ID(N'[dbo].[Manuscripts]'))
    CREATE NONCLUSTERED INDEX [IX_Manuscripts_ManuscriptSubmittedBy] ON [dbo].[Manuscripts] ([ManuscriptSubmittedBy] ASC);
GO

-- ---------------------------------------------------------------------------
-- ManuscriptAuthors  (FK → Manuscripts, Authors)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[ManuscriptAuthors]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ManuscriptAuthors]
    (
        [ManuscriptAuthorId]          INT             NOT NULL IDENTITY(1,1),
        [ManuscriptId]                INT             NOT NULL,
        [AuthorId]                    INT             NOT NULL,
        [ManuscriptAuthorType]        NVARCHAR(20)    NOT NULL
            CONSTRAINT [DF_ManuscriptAuthors_ManuscriptAuthorType]  DEFAULT (N'Co Author')
            CONSTRAINT [CK_ManuscriptAuthors_ManuscriptAuthorType]  CHECK (
                [ManuscriptAuthorType] IN (N'Main Author', N'Co Author')
            ),
        [ManuscriptAuthorStatus]      TINYINT         NOT NULL CONSTRAINT [DF_ManuscriptAuthors_ManuscriptAuthorStatus]      DEFAULT (1),
        [ManuscriptAuthorCreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_ManuscriptAuthors_ManuscriptAuthorCreatedDate] DEFAULT (GETUTCDATE()),
        [ManuscriptAuthorUpdatedDate] DATETIME2       NULL,

        CONSTRAINT [PK_ManuscriptAuthors] PRIMARY KEY CLUSTERED ([ManuscriptAuthorId] ASC),
        CONSTRAINT [FK_ManuscriptAuthors_Manuscripts] FOREIGN KEY ([ManuscriptId])
            REFERENCES [dbo].[Manuscripts] ([ManuscriptId]),
        CONSTRAINT [FK_ManuscriptAuthors_Authors] FOREIGN KEY ([AuthorId])
            REFERENCES [dbo].[Authors] ([AuthorId])
    );
    PRINT 'Table ManuscriptAuthors created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ManuscriptAuthors_ManuscriptId' AND object_id = OBJECT_ID(N'[dbo].[ManuscriptAuthors]'))
    CREATE NONCLUSTERED INDEX [IX_ManuscriptAuthors_ManuscriptId] ON [dbo].[ManuscriptAuthors] ([ManuscriptId] ASC);
GO

-- ---------------------------------------------------------------------------
-- SpecialIssuesEditors  (FK → SpecialIssues)
-- ---------------------------------------------------------------------------
IF OBJECT_ID(N'[dbo].[SpecialIssuesEditors]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SpecialIssuesEditors]
    (
        [IssueEditorId] BIGINT      NOT NULL IDENTITY(1,1),
        [EditorId]      BIGINT      NOT NULL,
        [IssueId]       BIGINT      NOT NULL,
        [CreatedDate]   DATETIME2   NOT NULL CONSTRAINT [DF_SpecialIssuesEditors_CreatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedDate]   DATETIME2   NOT NULL,

        CONSTRAINT [PK_SpecialIssuesEditors] PRIMARY KEY CLUSTERED ([IssueEditorId] ASC),
        CONSTRAINT [FK_SpecialIssuesEditors_SpecialIssues] FOREIGN KEY ([IssueId])
            REFERENCES [dbo].[SpecialIssues] ([IssueId])
    );
    PRINT 'Table SpecialIssuesEditors created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_SpecialIssuesEditors_IssueId' AND object_id = OBJECT_ID(N'[dbo].[SpecialIssuesEditors]'))
    CREATE NONCLUSTERED INDEX [IX_SpecialIssuesEditors_IssueId] ON [dbo].[SpecialIssuesEditors] ([IssueId] ASC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_SpecialIssuesEditors_EditorId' AND object_id = OBJECT_ID(N'[dbo].[SpecialIssuesEditors]'))
    CREATE NONCLUSTERED INDEX [IX_SpecialIssuesEditors_EditorId] ON [dbo].[SpecialIssuesEditors] ([EditorId] ASC);
GO

-- ===========================================================================
--  VIEWS  (CREATE OR ALTER — safe to re-run)
-- ===========================================================================

-- ---------------------------------------------------------------------------
-- vw_JournalDetails
-- ---------------------------------------------------------------------------
GO
CREATE OR ALTER VIEW [dbo].[vw_JournalDetails]
AS
SELECT
    j.[JournalId],
    j.[JournalName],
    j.[JournalDoi],
    j.[JournalSeoName],
    j.[JournalAcceptanceRate],
    j.[JournalDecision],
    j.[JournalAcceptancePublic],
    j.[JournalCitescore1],
    j.[JournalImpact],
    j.[JournalApc],
    j.[JournalSnip],
    j.[JournalSjr],
    j.[JournalCitescore],
    j.[Volume],
    j.[Issue],
    j.[Year],
    j.[Email],
    j.[Scholar],
    j.[IndexedArticle],
    j.[IndexingImage],
    j.[IndexingUrl],
    j.[ImpactFactor],
    j.[CrossrefUrl],
    j.[CrossrefImage],
    j.[IssnNumber],
    j.[YoutubeLink],
    j.[CallForPapers],
    j.[AimsAndScope],
    j.[ChiefEditor],
    j.[JournalImage],
    j.[IsExternalJournal],
    j.[JournalStatus],
    j.[JournalCreatedDate],
    j.[JournalUpdatedDate],
    e.[EditorName]        AS [ChiefEditorName],
    e.[EditorAddress]     AS [ChiefEditorAddress],
    e.[EditorDesignation],
    e.[Qualification],
    e.[ProfileImage]      AS [EditorProfileImage],
    e.[EditorPhone],
    e.[EditorEmail],
    e.[EditorOrcid],
    e.[EditorGoogleScholar],
    e.[EditorWebsite],
    e.[EditorBiography]   AS [Biography],
    e.[EditorResearch]    AS [Research]
FROM [dbo].[Journals] j
LEFT JOIN [dbo].[Editors] e ON j.[ChiefEditor] = e.[EditorId];
GO

-- ---------------------------------------------------------------------------
-- vw_JournalEditors
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_JournalEditors]
AS
SELECT
    jeb.[EditorialBoardId],
    jeb.[JournalId],
    jeb.[EditorId],
    jeb.[EditorRole],
    e.[EditorName],
    e.[EditorAddress],
    e.[EditorDesignation],
    e.[Qualification],
    e.[ProfileImage],
    e.[EditorEmail],
    e.[EditorOrcid],
    e.[EditorGoogleScholar],
    e.[EditorWebsite],
    e.[EditorPhone],
    j.[JournalName],
    j.[JournalSeoName],
    j.[JournalStatus],
    e.[EditorBiography]  AS [Biography],
    e.[EditorResearch]   AS [Research]
FROM [dbo].[JournalEditorialBoard] jeb
LEFT JOIN [dbo].[Editors]  e ON jeb.[EditorId]  = e.[EditorId]
LEFT JOIN [dbo].[Journals] j ON jeb.[JournalId] = j.[JournalId]
WHERE jeb.[EditorRole] <> N'Editor Profiles' OR jeb.[EditorRole] IS NULL;
GO

-- ---------------------------------------------------------------------------
-- vw_EditorProfiles
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_EditorProfiles]
AS
SELECT
    jeb.[EditorialBoardId],
    jeb.[JournalId],
    jeb.[EditorRole],
    ep.[EditorId],
    ep.[EditorName],
    ep.[EditorDesignation],
    ep.[EditorImage],
    ep.[EditorDescription],
    ep.[EditorStatus],
    j.[JournalName],
    j.[JournalSeoName],
    j.[JournalStatus]
FROM [dbo].[JournalEditorialBoard] jeb
LEFT JOIN [dbo].[EditorProfiles] ep ON jeb.[EditorId]  = ep.[EditorId]
LEFT JOIN [dbo].[Journals]       j  ON jeb.[JournalId] = j.[JournalId]
WHERE jeb.[EditorRole] = N'Editor Profiles';
GO

-- ---------------------------------------------------------------------------
-- vw_ArticleDetails
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_ArticleDetails]
AS
SELECT
    a.[ArticleId],
    a.[ArticleViews],
    a.[ArticleDownloads],
    a.[ArticleFor],
    a.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[IssnNumber],
    a.[VolumeNo],
    a.[IssueNo],
    a.[ArticleType],
    CASE a.[ArticleType]
        WHEN 0 THEN N'Article In Press'
        WHEN 1 THEN N'Current Issue'
        WHEN 2 THEN N'Archive'
        ELSE        N'Unknown'
    END                         AS [ArticleTypeLabel],
    a.[ArticleName],
    a.[ArticleSeoName],
    a.[ArticleDoi],
    a.[CorrespondingAuthor],
    a.[ReceivedDate],
    a.[AcceptedDate],
    a.[PublishedDate],
    a.[Citation],
    a.[Copyright],
    a.[ArticleInformation],
    a.[ArticleAbstract],
    a.[DisplayArticleInformation],
    a.[DisplayArticleAbstract],
    a.[AddressDesc],
    a.[AbstractKeywords],
    a.[ArticlePdf],
    a.[ArticleStatus],
    a.[ArticleCreatedDate],
    a.[ArticleUpdatedDate]
FROM [dbo].[Articles] a
INNER JOIN [dbo].[Journals] j ON a.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_ActiveArticles
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_ActiveArticles]
AS
SELECT
    a.[ArticleId],
    a.[ArticleViews],
    a.[ArticleDownloads],
    a.[ArticleFor],
    a.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    a.[VolumeNo],
    a.[IssueNo],
    a.[ArticleType],
    CASE a.[ArticleType]
        WHEN 0 THEN N'Article In Press'
        WHEN 1 THEN N'Current Issue'
        WHEN 2 THEN N'Archive'
        ELSE        N'Unknown'
    END                         AS [ArticleTypeLabel],
    a.[ArticleName],
    a.[ArticleSeoName],
    a.[ArticleDoi],
    a.[CorrespondingAuthor],
    a.[ReceivedDate],
    a.[AcceptedDate],
    a.[PublishedDate],
    a.[Citation],
    a.[Copyright],
    a.[DisplayArticleInformation],
    a.[DisplayArticleAbstract],
    a.[AddressDesc],
    a.[AbstractKeywords],
    a.[ArticlePdf],
    a.[ArticleCreatedDate],
    a.[ArticleUpdatedDate]
FROM [dbo].[Articles] a
INNER JOIN [dbo].[Journals] j ON a.[JournalId] = j.[JournalId]
WHERE
    a.[ArticleStatus]  = 1
    AND j.[JournalStatus] = 1;
GO

-- ---------------------------------------------------------------------------
-- vw_CurrentIssueArticles
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_CurrentIssueArticles]
AS
SELECT
    a.[ArticleId],
    a.[ArticleViews],
    a.[ArticleDownloads],
    a.[ArticleFor],
    a.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    a.[VolumeNo],
    a.[IssueNo],
    a.[ArticleName],
    a.[ArticleSeoName],
    a.[ArticleDoi],
    a.[CorrespondingAuthor],
    a.[ReceivedDate],
    a.[AcceptedDate],
    a.[PublishedDate],
    a.[Citation],
    a.[Copyright],
    a.[DisplayArticleInformation],
    a.[DisplayArticleAbstract],
    a.[AddressDesc],
    a.[AbstractKeywords],
    a.[ArticlePdf],
    a.[ArticleCreatedDate],
    a.[ArticleUpdatedDate]
FROM [dbo].[Articles] a
INNER JOIN [dbo].[Journals] j ON a.[JournalId] = j.[JournalId]
WHERE
    a.[ArticleType]    = 1      -- Current Issue
    AND a.[ArticleStatus]  = 1
    AND j.[JournalStatus] = 1;
GO

-- ---------------------------------------------------------------------------
-- vw_ArticlesInPress
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_ArticlesInPress]
AS
SELECT
    a.[ArticleId],
    a.[ArticleViews],
    a.[ArticleDownloads],
    a.[ArticleFor],
    a.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    a.[ArticleName],
    a.[ArticleSeoName],
    a.[ArticleDoi],
    a.[CorrespondingAuthor],
    a.[ReceivedDate],
    a.[AcceptedDate],
    a.[PublishedDate],
    a.[Citation],
    a.[Copyright],
    a.[DisplayArticleInformation],
    a.[DisplayArticleAbstract],
    a.[AddressDesc],
    a.[AbstractKeywords],
    a.[ArticlePdf],
    a.[ArticleCreatedDate],
    a.[ArticleUpdatedDate]
FROM [dbo].[Articles] a
INNER JOIN [dbo].[Journals] j ON a.[JournalId] = j.[JournalId]
WHERE
    a.[ArticleType]    = 0      -- Article In Press
    AND a.[ArticleStatus]  = 1
    AND j.[JournalStatus] = 1;
GO

-- ---------------------------------------------------------------------------
-- vw_ArchiveArticles
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_ArchiveArticles]
AS
SELECT
    a.[ArticleId],
    a.[ArticleViews],
    a.[ArticleDownloads],
    a.[ArticleFor],
    a.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    a.[VolumeNo],
    a.[IssueNo],
    a.[ArticleName],
    a.[ArticleSeoName],
    a.[ArticleDoi],
    a.[CorrespondingAuthor],
    a.[ReceivedDate],
    a.[AcceptedDate],
    a.[PublishedDate],
    a.[Citation],
    a.[Copyright],
    a.[DisplayArticleInformation],
    a.[DisplayArticleAbstract],
    a.[AddressDesc],
    a.[AbstractKeywords],
    a.[ArticlePdf],
    a.[ArticleCreatedDate],
    a.[ArticleUpdatedDate]
FROM [dbo].[Articles] a
INNER JOIN [dbo].[Journals] j ON a.[JournalId] = j.[JournalId]
WHERE
    a.[ArticleType]    = 2      -- Archive
    AND a.[ArticleStatus]  = 1
    AND j.[JournalStatus] = 1;
GO

-- ---------------------------------------------------------------------------
-- vw_JournalArticleCounts
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_JournalArticleCounts]
AS
SELECT
    j.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[IssnNumber],
    j.[JournalStatus],
    COUNT(a.[ArticleId])    AS [TotalArticles],
    SUM(CASE WHEN a.[ArticleType] = 0 AND a.[ArticleStatus] = 1 THEN 1 ELSE 0 END) AS [ArticlesInPress],
    SUM(CASE WHEN a.[ArticleType] = 1 AND a.[ArticleStatus] = 1 THEN 1 ELSE 0 END) AS [CurrentIssueArticles],
    SUM(CASE WHEN a.[ArticleType] = 2 AND a.[ArticleStatus] = 1 THEN 1 ELSE 0 END) AS [ArchiveArticles],
    SUM(ISNULL(a.[ArticleViews],     0))                                            AS [TotalViews],
    SUM(ISNULL(a.[ArticleDownloads], 0))                                            AS [TotalDownloads]
FROM [dbo].[Journals] j
LEFT JOIN [dbo].[Articles] a ON j.[JournalId] = a.[JournalId]
GROUP BY
    j.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[IssnNumber],
    j.[JournalStatus];
GO

-- ---------------------------------------------------------------------------
-- vw_AbstractingIndexingDetail
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_AbstractingIndexingDetail]
AS
SELECT
    ai.[AbstractionId],
    ai.[JournalId],
    j.[JournalName],
    j.[JournalSeoName],
    j.[JournalStatus],
    ai.[AbstractionUrl],
    ai.[AbstractionTitle],
    ai.[Status]  AS [AbstractionStatus],
    ai.[CreatedDate],
    ai.[UpdatedDate]
FROM [dbo].[AbstractingIndexing] ai
INNER JOIN [dbo].[Journals] j ON ai.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_ArticleProcessingCharges
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_ArticleProcessingCharges]
AS
SELECT
    apc.[ApcId],
    apc.[JournalId],
    apc.[ApcAmount],
    apc.[CreatedDate],
    apc.[UpdatedDate],
    j.[JournalName],
    j.[JournalSeoName]
FROM [dbo].[ArticleProcessingCharges] apc
LEFT JOIN [dbo].[Journals] j ON apc.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_Documents
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_Documents]
AS
SELECT
    d.[DocId],
    d.[Title],
    d.[PageSlug],
    d.[DocData],
    d.[JournalId],
    d.[Type],
    d.[SeoKeywords],
    d.[Status],
    d.[CreatedBy],
    d.[CreatedDate],
    d.[UpdatedDate],
    j.[JournalName],
    j.[JournalSeoName]
FROM [dbo].[Documents] d
LEFT JOIN [dbo].[Journals] j ON d.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_Manuscripts
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_Manuscripts]
AS
SELECT
    m.[ManuscriptId],
    m.[JournalId],
    m.[ManuscriptSubmittedBy],
    m.[ManuscriptTitle],
    m.[ManuscriptType],
    m.[MessageToEditor],
    m.[ManuscriptFile],
    m.[ManuscriptStatus],
    m.[ManuscriptCreatedDate],
    m.[ManuscriptUpdatedDate],
    j.[JournalName],
    a.[AuthorName]
FROM [dbo].[Manuscripts] m
INNER JOIN [dbo].[Journals] j ON m.[JournalId]             = j.[JournalId]
INNER JOIN [dbo].[Authors]  a ON m.[ManuscriptSubmittedBy] = a.[AuthorId];
GO

-- ---------------------------------------------------------------------------
-- vw_ManuscriptAuthors
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_ManuscriptAuthors]
AS
SELECT
    ma.[ManuscriptAuthorId],
    ma.[ManuscriptId],
    ma.[AuthorId],
    ma.[ManuscriptAuthorType],
    ma.[ManuscriptAuthorStatus],
    ma.[ManuscriptAuthorCreatedDate],
    ma.[ManuscriptAuthorUpdatedDate],
    a.[AuthorName]
FROM [dbo].[ManuscriptAuthors] ma
INNER JOIN [dbo].[Authors] a ON ma.[AuthorId] = a.[AuthorId];
GO

-- ---------------------------------------------------------------------------
-- vw_ManuscriptRequest
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_ManuscriptRequest]
AS
SELECT
    mr.[RequestId]                              AS [ManuscriptId],
    CONCAT(mr.[FirstName], N' ', mr.[LastName]) AS [Name],
    mr.[Email],
    mr.[JournalId],
    mr.[Phone],
    mr.[Country],
    mr.[Orcid],
    mr.[Address],
    mr.[ManuscriptTitle]                        AS [Title],
    mr.[ArticleType],
    mr.[Abstract],
    mr.[Keywords],
    mr.[CoverLetter],
    mr.[Files],
    j.[JournalName],
    j.[JournalSeoName],
    mr.[Status],
    mr.[CreatedDate],
    mr.[UpdatedDate]
FROM [dbo].[ManuscriptRequest] mr
LEFT JOIN [dbo].[Journals] j ON mr.[JournalId] = j.[JournalId]
WHERE mr.[JournalId] IS NOT NULL AND mr.[JournalId] > 0;
GO

-- ---------------------------------------------------------------------------
-- vw_MemberedIn
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_MemberedIn]
AS
SELECT
    mi.[Id],
    mi.[JournalId],
    j.[JournalName],
    mi.[Name],
    mi.[Image],
    mi.[Url],
    mi.[Status],
    mi.[CreatedDate],
    mi.[UpdatedDate]
FROM [dbo].[MemberedIn] mi
LEFT JOIN [dbo].[Journals] j ON mi.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_MemberRequest
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_MemberRequest]
AS
SELECT
    mrr.[Id]          AS [RequestId],
    mrr.[Name],
    mrr.[Email],
    mrr.[PhoneNumber],
    mrr.[Designation],
    mrr.[Country],
    mrr.[Type],
    mrr.[CreatedDate],
    j.[JournalName],
    j.[JournalStatus]
FROM [dbo].[MemberRequestReviewer] mrr
LEFT JOIN [dbo].[Journals] j ON mrr.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_PubmedIndex
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_PubmedIndex]
AS
SELECT
    pi.[PubmedIndexId],
    pi.[JournalId],
    pi.[IndexText],
    pi.[PubmedUrl],
    pi.[PmcUrl],
    pi.[CreatedDate],
    pi.[UpdatedDate],
    j.[JournalName],
    j.[JournalSeoName]
FROM [dbo].[PubmedIndex] pi
LEFT JOIN [dbo].[Journals] j ON pi.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_SpecialIssues  (list view – no subquery)
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_SpecialIssues]
AS
SELECT
    s.[IssueId],
    s.[Title],
    s.[JournalId],
    s.[Slug],
    s.[DeadlineDate],
    s.[ViewCount],
    s.[IssnNumber],
    s.[CitiesScore],
    s.[ImpactFactor],
    s.[FileLink],
    s.[IssueData],
    s.[Keywords],
    s.[Benefits],
    s.[CreatedDate],
    s.[UpdatedDate],
    s.[Status],
    j.[JournalName],
    j.[JournalSeoName]
FROM [dbo].[SpecialIssues] s
LEFT JOIN [dbo].[Journals] j ON s.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_SpecialIssue  (detail view – includes editors JSON subquery)
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_SpecialIssue]
AS
SELECT
    s.[IssueId],
    s.[Title],
    s.[JournalId],
    s.[Slug],
    s.[DeadlineDate],
    s.[ViewCount],
    s.[IssnNumber],
    s.[CitiesScore],
    s.[ImpactFactor],
    s.[FileLink],
    s.[IssueData],
    s.[IssueDataDisplay],
    s.[Keywords],
    s.[Benefits],
    s.[CreatedDate],
    s.[UpdatedDate],
    s.[Status],
    j.[JournalName],
    j.[JournalSeoName],
    (
        SELECT sie.[EditorId] AS editor_id, e.[EditorName] AS editor_name
        FROM   [dbo].[SpecialIssuesEditors] sie
        INNER JOIN [dbo].[Editors] e ON sie.[EditorId] = e.[EditorId]
        WHERE  sie.[IssueId] = s.[IssueId]
        FOR JSON PATH
    ) AS [EditorsData]
FROM [dbo].[SpecialIssues] s
LEFT JOIN [dbo].[Journals] j ON s.[JournalId] = j.[JournalId];
GO

-- ---------------------------------------------------------------------------
-- vw_SpecialIssueEditors
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_SpecialIssueEditors]
AS
SELECT
    sie.[IssueEditorId],
    sie.[IssueId],
    e.[EditorId],
    e.[EditorName],
    e.[EditorDesignation],
    e.[Qualification],
    e.[ProfileImage],
    e.[EditorEmail],
    e.[EditorPhone],
    e.[EditorAddress],
    e.[EditorBiography],
    e.[EditorResearch],
    e.[EditorStatus],
    e.[EditorOrcid],
    e.[EditorGoogleScholar],
    e.[EditorWebsite],
    e.[EditorCreatedDate],
    e.[EditorUpdatedDate]
FROM [dbo].[SpecialIssuesEditors] sie
LEFT JOIN [dbo].[Editors] e ON sie.[EditorId] = e.[EditorId];
GO

-- ---------------------------------------------------------------------------
-- vw_SubscriptionList
-- ---------------------------------------------------------------------------
CREATE OR ALTER VIEW [dbo].[vw_SubscriptionList]
AS
SELECT
    sl.[SubscriptionId],
    sl.[EmailId],
    sl.[SubscribedFor],
    sl.[SubscribedForId],
    sl.[SubscriptionStatus],
    sl.[CreatedDate],
    sl.[UpdatedDate],
    j.[JournalName]
FROM [dbo].[SubscriptionList] sl
LEFT JOIN [dbo].[Journals] j ON sl.[SubscribedForId] = j.[JournalId];
GO

-- ===========================================================================
PRINT '== AuctoresOnlineDB deployment complete ==';
PRINT '   35 tables | 22 views';
GO
