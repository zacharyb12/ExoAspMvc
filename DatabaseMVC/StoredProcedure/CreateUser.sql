CREATE PROCEDURE [dbo].[CreateUser]
	@Firstname varchar(50),
	@Lastname varchar(50),
	@Email varchar(50),
	@Pseudo varchar(50),
	@Password varchar(50)
AS
	BEGIN

	DECLARE @Salt VARCHAR(50)
	DECLARE @HashedPassword VARBINARY(64)
	DECLARE @IdTable TABLE (Id integer)

	SET @Salt = CONVERT(VARCHAR(50),NEWID())
	SET @HashedPassword = HASHBYTES('SHA2_512',CONCAT(@Salt,@Password))

	INSERT INTO User_ 
	(Firstname,Lastname,Email,Pseudo,Password,Salt)
	OUTPUT inserted.Id into @IdTable
	VALUES 
	(@Firstname,@Lastname,@Email,@Pseudo,@HashedPassword,@Salt)

	SELECT Id FROM @IdTable
	END

