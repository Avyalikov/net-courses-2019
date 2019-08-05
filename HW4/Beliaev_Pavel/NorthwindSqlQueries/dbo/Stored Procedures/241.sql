CREATE PROCEDURE [dbo].[241]
AS
	SELECT CompanyName
	FROM Suppliers
	WHERE 0 in (SELECT UnitsInStock FROM Products WHERE Products.SupplierID = Suppliers.SupplierID)
RETURN 0
