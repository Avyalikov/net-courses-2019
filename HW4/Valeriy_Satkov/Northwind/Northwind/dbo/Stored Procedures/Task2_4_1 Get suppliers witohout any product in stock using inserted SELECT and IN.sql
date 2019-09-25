CREATE PROCEDURE [dbo].[Task2_4_1 Get suppliers witohout any product in stock using inserted SELECT and IN]
AS
	SELECT DISTINCT	
		(SELECT suppls.CompanyName 
			FROM Suppliers AS suppls 
			WHERE suppls.SupplierID IN (prducts.SupplierID)) AS 'Suppliers without product'
	FROM Products AS prducts
	WHERE UnitsInStock = 0
