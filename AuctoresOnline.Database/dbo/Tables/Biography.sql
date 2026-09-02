CREATE TABLE [dbo].[Biography]
(
    [Id]           INT             NOT NULL IDENTITY(1,1),
    -- References Editors.EditorId
    [ProfeId]      INT             NOT NULL,
    [Biography]    NVARCHAR(MAX)   NOT NULL,
    [Status]       NVARCHAR(100)   NOT NULL CONSTRAINT [DF_Biography_Status] DEFAULT (N'Inactive'),
    [CreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_Biography_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]  DATETIME2       NOT NULL,

    CONSTRAINT [PK_Biography] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Biography_ProfeId]
    ON [dbo].[Biography] ([ProfeId] ASC);
GO
