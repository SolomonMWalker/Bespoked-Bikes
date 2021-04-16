CREATE TABLE [dbo].[Discount]
(
	[DiscountId] INT NOT NULL PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [BeginDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [DiscountPercentage] DECIMAL(5, 4) NOT NULL, 
    CONSTRAINT [FK_Product_ProductId_To_Discount_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product]([ProductId])
)
