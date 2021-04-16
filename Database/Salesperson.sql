CREATE TABLE [dbo].[Salesperson]
(
	[SalespersonId] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(200) NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [StartDate] DATE NOT NULL, 
    [TerminationDate] DATE NULL, 
    [Manager] NVARCHAR(50) NOT NULL
)
