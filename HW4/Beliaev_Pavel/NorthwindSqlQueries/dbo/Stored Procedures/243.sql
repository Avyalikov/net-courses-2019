CREATE PROCEDURE [dbo].[243]
AS
	SELECT CompanyName
	FROM Customers
	WHERE NOT EXISTS (SELECT OrderId FROM Orders WHERE Orders.CustomerID = Customers.CustomerID)
RETURN 0
