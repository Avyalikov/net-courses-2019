CREATE PROCEDURE [dbo].[1_1_1_DeliveredOrders]
AS
	SELECT OrderID, ShippedDate, ShipVia
    FROM Orders
    WHERE ShippedDate >= '1998-05-06' AND ShipVia >= 2
