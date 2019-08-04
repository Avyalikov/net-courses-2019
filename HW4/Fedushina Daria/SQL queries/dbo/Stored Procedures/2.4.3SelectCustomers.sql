CREATE PROCEDURE [dbo].[243SelectCustomers]
AS
BEGIN

SELECT [Customers].[ContactName]
FROM [Customers]
WHERE NOT EXISTS 
(SELECT [Orders].[CustomerID] 
FROM [Orders]
WHERE [Orders].[CustomerID]=[Customers].[CustomerID])
END
