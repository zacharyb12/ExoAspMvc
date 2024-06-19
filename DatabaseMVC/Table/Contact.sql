CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL , 
    [Password] NVARCHAR(50) NOT NULL
)
