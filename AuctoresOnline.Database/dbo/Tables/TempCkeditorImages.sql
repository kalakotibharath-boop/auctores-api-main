CREATE TABLE [dbo].[TempCkeditorImages]
(
    [TempCkeditorImageId]  INT             NOT NULL IDENTITY(1,1),
    [ArticleId]            INT             NULL,
    [DataOf]               NVARCHAR(100)   NULL,
    [FileName]             NVARCHAR(255)   NOT NULL,
    [FileUrl]              NVARCHAR(500)   NOT NULL,
    [FileSaved]            NVARCHAR(3)     NOT NULL
        CONSTRAINT [DF_TempCkeditorImages_FileSaved] DEFAULT (N'no')
        CONSTRAINT [CK_TempCkeditorImages_FileSaved] CHECK ([FileSaved] IN (N'yes', N'no')),
    [CreatedDate]          DATETIME2       NOT NULL CONSTRAINT [DF_TempCkeditorImages_CreatedDate] DEFAULT (GETUTCDATE()),

    CONSTRAINT [PK_TempCkeditorImages] PRIMARY KEY CLUSTERED ([TempCkeditorImageId] ASC)
);
GO
