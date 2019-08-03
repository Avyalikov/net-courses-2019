CREATE PROCEDURE [dbo].[Procedure_2.4.1]
AS
	SELECT CompanyName
	FROM Suppliers Sup
	WHERE Sup.SupplierID IN
		(SELECT SupplierID FROM Products
	WHERE UnitsInStock = 0)
