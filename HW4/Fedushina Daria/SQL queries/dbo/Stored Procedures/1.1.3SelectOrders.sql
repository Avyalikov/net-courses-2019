CREATE PROCEDURE [dbo].[113SelectOrders](
	@shipDate DATETIME)
AS 
BEGIN

SELECT [OrderID] as "Order Number", CASE
WHEN [ShippedDate] is NULL THEN 'Not Shipped' ELSE CAST([ShippedDate] AS nvarchar)
END AS [ShippedDate]
FROM [dbo].[Orders]
WHERE [ShippedDate] > @shipDate OR [ShippedDate] Is NULL

END

--EXECUTE Northwind.dbo.[113SelectOrders] '1998-05-06 00:00:00.000'


