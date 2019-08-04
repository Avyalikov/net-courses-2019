CREATE PROCEDURE [dbo].[243CustomersWithoutOrders]
AS
	SELECT ContactName
FROM Customers
WHERE NOT EXISTS 
      (SELECT ContactName 
	  FROM Orders 
	  WHERE Customers.CustomerID = Orders.CustomerID)