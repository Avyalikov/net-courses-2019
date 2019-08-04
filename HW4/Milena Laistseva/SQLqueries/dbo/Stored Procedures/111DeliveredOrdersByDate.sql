CREATE PROCEDURE [dbo].[111DeliveredOrdersByDate]
AS
	SELECT OrderID, ShippedDate, ShipVia
	FROM Orders
	WHERE ShippedDate >= '1998-05-06' AND ShipVia >= 2

