CREATE PROCEDURE [dbo].[Login]
    @Email VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    -- Check if user exists with the given email
    IF EXISTS (SELECT * FROM [User_] WHERE Email = @Email)
    BEGIN
        -- If the user exists, return the user ID if the password is correct
        SELECT Id 
        FROM [User_] 
        WHERE Email = @Email 
        AND Password = HASHBYTES('SHA2_512', CONCAT(Salt, @Password))

    END
END