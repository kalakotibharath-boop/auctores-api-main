CREATE TABLE [dbo].[Admin]
(
    [Id]          INT             NOT NULL IDENTITY(1,1),
    [Name]        NVARCHAR(200)   NOT NULL,
    [Email]       NVARCHAR(200)   NOT NULL,
    [Mobile]      NVARCHAR(100)   NOT NULL,
    [Username]    NVARCHAR(100)   NOT NULL,
    [Password]    NVARCHAR(256)   NOT NULL,
    -- 0 = auctores-admin, 1 = editor, 2 = super-admin
    [RoleType]    SMALLINT        NOT NULL CONSTRAINT [DF_Admin_RoleType] DEFAULT (0),
    [CreatedDate] DATETIME2       NOT NULL CONSTRAINT [DF_Admin_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate] DATETIME2       NULL,

    CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX_Admin_Username]
    ON [dbo].[Admin] ([Username] ASC);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX_Admin_Email]
    ON [dbo].[Admin] ([Email] ASC);
GO
