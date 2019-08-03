CREATE PROCEDURE [dbo].[Procedure_1.1.1]
AS
	SELECT Orders.OrderID, Orders.ShipVia, Orders.ShippedDate 
	FROM Orders
	WHERE ShipVia >= 2 AND ShippedDate >= '1998-05-06'
