CREATE PROCEDURE [dbo].[Proc_2_4_1]
AS
	SELECT CompanyName
	FROM Suppliers
	WHERE 0 in (SELECT UnitsInStock FROM Products WHERE Products.SupplierID = Suppliers.SupplierID)
RETURN 0
