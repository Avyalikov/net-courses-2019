CREATE PROCEDURE [dbo].[2_3_2_SumOrders]
AS
	SELECT cust.ContactName, 
		COUNT(ord.OrderID) as Counter
    FROM Customers as cust 
	LEFT JOIN Orders as ord
    ON cust.CustomerID = ord.CustomerID
    GROUP BY ContactName
	ORDER BY Counter
