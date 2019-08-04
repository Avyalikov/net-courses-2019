CREATE PROCEDURE [dbo].[query_1_1_1]
AS
	SELECT OrderID as 'Order', ShippedDate as 'Shipped Date', ShipVia as 'Ship via'
	FROM Orders
	WHERE ShipVia >= 2 AND ShippedDate >= '1998-05-06'
