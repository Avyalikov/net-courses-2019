CREATE PROCEDURE [dbo].[_243CustomersWithoutOrders]
AS
	SELECT c.ContactName
    FROM Customers as c
    WHERE NOT EXISTS 
	     (SELECT c.ContactName
         FROM Orders as o
         WHERE c.CustomerID=o.CustomerID)