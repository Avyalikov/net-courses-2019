-- Task 2.4 #1
CREATE PROCEDURE SuppliersUnitsInStock
AS
SELECT 
	Suppliers.CompanyName
FROM
	dbo.Suppliers 
WHERE 
	Suppliers.SupplierID IN
		(SELECT 
			SupplierID
		FROM
			dbo.Products
		WHERE
			Products.UnitsInStock = 0)