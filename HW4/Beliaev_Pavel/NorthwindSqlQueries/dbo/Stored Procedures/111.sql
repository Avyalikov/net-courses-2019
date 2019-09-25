CREATE PROCEDURE [dbo].[111]
	@Date date = '1998-05-06'
AS
	SELECT OrderID, ShippedDate, ShipVia
	FROM dbo.Orders
	WHERE ShippedDate >= @Date AND ShipVia >= 2
RETURN 0
