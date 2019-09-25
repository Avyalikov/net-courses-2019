CREATE PROCEDURE [dbo].[Proc_2_4_3]
AS
	SELECT CompanyName
	FROM Customers
	WHERE NOT EXISTS (SELECT OrderId FROM Orders WHERE Orders.CustomerID = Customers.CustomerID)
RETURN 0
