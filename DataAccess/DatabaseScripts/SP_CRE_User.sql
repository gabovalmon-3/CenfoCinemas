-- SP que crea un usuario
CREATE PROCEDURE CRE_USER_PR
	@P_UserCode nvarchar(30), 
	@P_Name nvarchar(50),
	@P_Email nvarchar(30),
	@P_Password nvarchar(50),
	@P_BirthDate datetime,
	@P_Status nvarchar(10)
AS
BEGIN
    INSERT INTO TBL_User(Created, UserCode, Name, Email, Password, BirthDate, Status)
	VALUES (GETDATE(), @P_UserCode, @P_Name, @P_Email, @P_Password, @P_BirthDate, @P_Status)
END
GO