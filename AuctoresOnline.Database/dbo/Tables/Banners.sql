CREATE TABLE [dbo].[Banners]
(
    [BannerId]           INT             NOT NULL IDENTITY(1,1),
    [BannerHeading]      NVARCHAR(100)   NOT NULL,
    [BannerDescription]  NVARCHAR(MAX)   NOT NULL,
    [BannerImage]        NVARCHAR(200)   NULL,
    -- 0 = Inactive, 1 = Active
    [BannerStatus]       TINYINT         NOT NULL CONSTRAINT [DF_Banners_BannerStatus] DEFAULT (1),
    [CreatedDate]        DATETIME2       NOT NULL CONSTRAINT [DF_Banners_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]        DATETIME2       NOT NULL,

    CONSTRAINT [PK_Banners] PRIMARY KEY CLUSTERED ([BannerId] ASC)
);
GO
