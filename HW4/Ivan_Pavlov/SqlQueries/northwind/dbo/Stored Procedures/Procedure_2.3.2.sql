CREATE PROCEDURE [dbo].[Procedure_2.3.2]
AS
	SELECT ContactName,
		COUNT(OrderID) AS 'Total'
	FROM Customers Left JOIN Orders 
	ON Customers.CustomerID = Orders.CustomerID
	GROUP BY ContactName
	ORDER BY Total
