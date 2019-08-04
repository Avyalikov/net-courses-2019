
CREATE PROCEDURE [dbo].[111SelectOrders](
	@shipDate DATETIME,
	@shipVia int)        
AS
BEGIN

SELECT [OrderID], [ShippedDate], [ShipVia]
FROM [dbo].[Orders]
WHERE [ShippedDate] >= @shipDate
AND  [ShipVia] >= @shipVia

END

  -- EXECUTE Northwind.dbo.[111SelectOrders] '1998-05-06 00:00:00.000' , 2
