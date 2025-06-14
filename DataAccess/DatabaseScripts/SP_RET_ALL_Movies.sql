
CREATE PROCEDURE RET_ALL_MOVIE_PR
AS
    SELECT 
        Id,
        Created,
        Updated,
        Title,
        Description,
        ReleaseDate,
        Genre,
        Director
    FROM TBL_Movie;
GO
