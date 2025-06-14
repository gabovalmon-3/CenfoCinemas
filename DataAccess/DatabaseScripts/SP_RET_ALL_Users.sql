
CREATE PROCEDURE RET_ALL_USER_PR
AS
    SELECT 
        Id,
        Created,
        Updated,
        UserCode,
        Name,
        Email,
        Password,
        BirthDate,
        Status
    FROM TBL_User;
GO
