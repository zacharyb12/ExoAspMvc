/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO CONTACT (Lastname,Firstname,Email,Password)
VALUES
( "john", "Doe","JohnDoe@mail.com","password") ,
( jane, "Doe","JaneDoe@mail.com","password") ,
(  "Mini", "Doe","miniDoe@mail.com","password") 