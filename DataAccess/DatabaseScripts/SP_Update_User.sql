
CREATE PROCEDURE UPD_USER_PR
    @P_UserCode NVARCHAR(50),
    @P_Name     NVARCHAR(100),
    @P_Email    NVARCHAR(100),
    @P_Status   NVARCHAR(5)
AS
    UPDATE TBL_User
    SET 
        Name   = @P_Name,
        Email  = @P_Email,
        Status = @P_Status
    WHERE UserCode = @P_UserCode;
GO
