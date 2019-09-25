CREATE PROCEDURE [dbo].[232CustomersJoinOrders] 
AS
BEGIN

SELECT [Customers].[ContactName] AS 'Customers', COUNT([Orders].[OrderID]) AS 'Orders'
FROM 
	[Customers]
	LEFT JOIN [Orders]
	ON [Customers].[CustomerID]=[Orders].[CustomerID]
	GROUP BY [ContactName]
	ORDER BY 'Orders' ASC
END
