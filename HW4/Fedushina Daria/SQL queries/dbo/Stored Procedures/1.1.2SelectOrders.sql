CREATE PROCEDURE [dbo].[112SelectOrders]
AS 
BEGIN

SELECT [OrderID], CASE
WHEN [ShippedDate] is NULL THEN 'Not Shipped' ELSE CAST([ShippedDate] AS nvarchar)
END AS [ShippedDate]
FROM [dbo].[Orders]
WHERE
[ShippedDate] IS NULL 

END


