ALTER PROCEDURE UserRegistration
(
    @FullName varchar(50),
    @EmailId varchar(50),
    @Password varchar(50),
    @MobileNumber varchar(20)
)
AS
BEGIN
    -- Modify the existing logic as needed
    INSERT INTO UserRegister (FullName, EmailId, Password, MobileNumber)
    VALUES (@FullName, @EmailId, @Password, @MobileNumber)
END

-- Create the stored procedure named 'GetUserByEmail'
CREATE PROCEDURE GetUser
    @EmailId NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        UserId,
        FullName,
        EmailId,
        [password] AS Password,
        MobileNumber
    FROM 
        UserRegister
    WHERE 
        EmailId = @EmailId;
END

DROP PROCEDURE GetUser;

-- GetUser stored procedure
CREATE PROCEDURE GetUser
(
    @EmailId varchar(50)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        UserId,
        FullName,
        EmailId,
        Password,
        MobileNumber
    FROM 
        UserRegister
    WHERE 
        EmailId = @EmailId;
END

CREATE PROCEDURE GetUser(
@EmailId varchar(20)
)
As
Begin Select * from UserRegister where EmailId=@EmailId
End


select * from UserRegister