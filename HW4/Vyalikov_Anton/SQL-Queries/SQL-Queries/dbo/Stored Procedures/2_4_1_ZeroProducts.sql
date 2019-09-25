CREATE PROCEDURE [dbo].[2_4_1_ZeroProducts]
AS
	SELECT sup.CompanyName
    FROM Suppliers as sup
    WHERE sup.SupplierID IN 
	  (SELECT SupplierID
      FROM Products
      WHERE UnitsInStock = 0)
