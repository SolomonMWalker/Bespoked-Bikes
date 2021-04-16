CREATE TABLE [dbo].[Sale]
(
	[SaleId] INT NOT NULL PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [SalespersonId] INT NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [SalesDate] DATE NOT NULL, 
    CONSTRAINT [FK_Product_ProductId_To_Sale_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]),
    CONSTRAINT [FK_Salesperson_SalespersonId_To_Sale_SalespersonId] FOREIGN KEY ([SalespersonId]) REFERENCES [Salesperson] ([SalespersonId]),
    CONSTRAINT [FK_Customer_CustomerId_To_Sale_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([CustomerId]) 
)
