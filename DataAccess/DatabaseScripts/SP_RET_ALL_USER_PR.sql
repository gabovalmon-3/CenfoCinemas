CREATE PROCEDURE RET_ALL_USER_PR
AS
BEGIN
    SELECT Id, Created, Updated, UserCode, Name, Email, Password, BirthDate, Status
	FROM TBL_User;
END
GO