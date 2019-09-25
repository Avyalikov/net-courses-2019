CREATE PROCEDURE [dbo].[213CountOrders] 
AS 
BEGIN

SELECT COUNT(DISTINCT [CustomerID]) AS CountCustomers
FROM [Orders]

END





