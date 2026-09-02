CREATE TABLE [dbo].[Testimonials]
(
    [TestimonialId]           INT             NOT NULL IDENTITY(1,1),
    [TestimonialName]         NVARCHAR(100)   NOT NULL,
    [TestimonialImage]        NVARCHAR(100)   NOT NULL,
    [TestimonialDescription]  NVARCHAR(MAX)   NOT NULL,
    -- 0 = Inactive, 1 = Active
    [TestimonialStatus]       TINYINT         NOT NULL,
    [TestimonialCreatedDate]  DATETIME2       NOT NULL CONSTRAINT [DF_Testimonials_TestimonialCreatedDate] DEFAULT (GETUTCDATE()),
    [TestimonialUpdatedDate]  DATETIME2       NULL,

    CONSTRAINT [PK_Testimonials] PRIMARY KEY CLUSTERED ([TestimonialId] ASC)
);
GO
