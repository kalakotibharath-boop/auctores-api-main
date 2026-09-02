CREATE TABLE [dbo].[Collaborators]
(
    [CollaboratorId]      INT             NOT NULL IDENTITY(1,1),
    [CollaboratorName]    NVARCHAR(100)   NOT NULL,
    [CollaboratorUrl]     NVARCHAR(MAX)   NOT NULL,
    [CollaboratorImage]   NVARCHAR(200)   NULL,
    -- 0 = Inactive, 1 = Active
    [CollaboratorStatus]  TINYINT         NOT NULL CONSTRAINT [DF_Collaborators_CollaboratorStatus] DEFAULT (1),
    [CreatedDate]         DATETIME2       NOT NULL CONSTRAINT [DF_Collaborators_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]         DATETIME2       NULL,

    CONSTRAINT [PK_Collaborators] PRIMARY KEY CLUSTERED ([CollaboratorId] ASC)
);
GO
