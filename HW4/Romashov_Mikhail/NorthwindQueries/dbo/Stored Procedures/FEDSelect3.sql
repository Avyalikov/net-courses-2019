CREATE PROCEDURE [dbo].[FEDSelect3] AS 
SELECT [OrderID] as "Order Number", [ShippedDate] as "Shipped Date"
FROM [dbo].[Orders]
WHERE [ShippedDate] >= '1998-05-06 00:00:00.000' OR [ShippedDate] Is NULL
SELECT CASE
WHEN [ShippedDate] is NULL THEN 'Not Shipped' ELSE CAST([ShippedDate] AS DATETIME)
END AS [ShippedDate]
FROM [dbo].[Orders]



