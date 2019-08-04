CREATE PROCEDURE [dbo].[243SelectCustomers]
AS
BEGIN

SELECT [Customers].[ContactName]
FROM [Customers]
WHERE [Customers].[CustomerID] 
NOT IN  
(SELECT [Orders].[CustomerID] 
FROM [Orders]
WHERE [Orders].[CustomerID]=[Customers].[CustomerID])
END
