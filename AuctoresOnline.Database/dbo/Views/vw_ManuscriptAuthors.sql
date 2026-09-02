CREATE VIEW [dbo].[vw_ManuscriptAuthors]
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
