CREATE PROCEDURE [dbo].[Task1_1_1 Get orders after Date incl and ShipVia over 2]
	@date datetime = '1998-05-06'	
AS
	SELECT OrderID, ShippedDate, ShipVia
	FROM Orders
	WHERE ShippedDate >= @date AND ShipVia >= 2
