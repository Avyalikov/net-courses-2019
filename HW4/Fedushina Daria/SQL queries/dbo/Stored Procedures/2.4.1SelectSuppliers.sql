CREATE PROCEDURE [dbo].[241SelectSuppliers]
AS
BEGIN

SELECT [CompanyName]
FROM [Suppliers]
WHERE [Suppliers].[SupplierID] 
IN  
(SELECT [Products].[SupplierID]
FROM [Products]
WHERE [Products].[UnitsInStock] = 0)
END

