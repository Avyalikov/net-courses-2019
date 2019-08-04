CREATE PROCEDURE [query_2_4_1]
AS	
	SELECT CompanyName
	FROM Suppliers
	WHERE SupplierID IN 
	(SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0)
