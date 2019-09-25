--Выбрать в таблице Orders заказы, 
--которые были доставлены после 6 мая 1998 года (колонка ShippedDate) 
--включительно и которые доставлены с ShipVia >= 2. Запрос должен 
--возвращать только колонки OrderID, ShippedDate и ShipVia.

CREATE PROCEDURE [1.1.1]
AS

SELECT OrderID, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate >= '1998-05-06 00:00:00.000' AND ShipVia >= 2