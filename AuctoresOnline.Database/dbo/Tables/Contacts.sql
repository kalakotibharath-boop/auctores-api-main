CREATE TABLE [dbo].[Contacts]
(
    [ContactId]       INT             NOT NULL IDENTITY(1,1),
    [ContactName]     NVARCHAR(100)   NOT NULL,
    [ContactEmail]    NVARCHAR(255)   NOT NULL,
    [ContactPhone]    NVARCHAR(20)    NOT NULL,
    [ContactSubject]  NVARCHAR(100)   NOT NULL,
    [ContactMessage]  NVARCHAR(MAX)   NOT NULL,
    [ContactStatus]   NVARCHAR(20)    NOT NULL
        CONSTRAINT [DF_Contacts_ContactStatus] DEFAULT (N'pending')
        CONSTRAINT [CK_Contacts_ContactStatus] CHECK (
            [ContactStatus] IN (N'pending', N'contacted', N'cancelled')
        ),
    [CreatedDate]     DATETIME2       NOT NULL CONSTRAINT [DF_Contacts_CreatedDate] DEFAULT (GETUTCDATE()),
    [UpdatedDate]     DATETIME2       NOT NULL,

    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ContactId] ASC)
);
GO
