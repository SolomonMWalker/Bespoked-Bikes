CREATE TABLE [dbo].[Product]
(
	[ProductId] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Manufacturer] NVARCHAR(50) NULL, 
    [Style] NVARCHAR(50) NULL, 
    [PurchasePrice] MONEY NOT NULL, 
    [SalePrice] MONEY NOT NULL, 
    [QtyOnHand] INT NOT NULL, 
    [CommissionPercentage] DECIMAL NOT NULL
)
