
CREATE PROCEDURE [dbo].[SelectOrders1.1] AS
SELECT [OrderID], [ShippedDate], [ShipVia]
FROM [dbo].[Orders]
WHERE [ShippedDate] >= '1998-05-06 00:00:00.000'
AND  [ShipVia] >=2


