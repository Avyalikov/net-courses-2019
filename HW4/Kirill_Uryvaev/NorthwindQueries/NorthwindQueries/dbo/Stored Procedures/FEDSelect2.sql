CREATE PROCEDURE [dbo].[FEDSelect2] AS 
SELECT [OrderID], [ShippedDate] 
FROM [dbo].[Orders]
SELECT CASE
WHEN [ShippedDate] is NULL THEN 'Not Shipped' ELSE CAST([ShippedDate] AS DATETIME)
END AS [ShippedDate]
FROM [dbo].[Orders]



