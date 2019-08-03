CREATE PROCEDURE [dbo].[_241SuppliersProductsWith0]
AS
	SELECT s.CompanyName
    FROM Suppliers as s
    WHERE s.SupplierID IN (SELECT SupplierID
    FROM Products
    WHERE UnitsInStock=0)