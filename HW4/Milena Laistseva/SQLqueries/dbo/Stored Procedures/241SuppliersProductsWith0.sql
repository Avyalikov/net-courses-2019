CREATE PROCEDURE [dbo].[241SuppliersProductsWith0]
AS
	SELECT CompanyName
FROM Suppliers
WHERE Suppliers.SupplierID IN (SELECT SupplierID FROM Products WHERE UnitsInStock = 0)