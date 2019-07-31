USE Northwind;
DECLARE @datetime2 datetime2 = '1996-07-16';
SELECT OrderID, ShippedDate, ShipVia FROM Orders
WHERE ShippedDate >= @datetime2 AND ShipVia >= 2;
