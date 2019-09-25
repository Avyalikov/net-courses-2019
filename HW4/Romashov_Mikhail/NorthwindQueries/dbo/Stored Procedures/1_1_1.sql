--Выбрать в таблице Orders заказы, которые были доставлены после 
--6 мая 1998 года (колонка ShippedDate) включительно и которые доставлены с ShipVia >= 2. 
--Запрос должен возвращать только колонки OrderID, ShippedDate и ShipVia.

CREATE PROCEDURE [query_1_1_1]
AS

SELECT OrderID as 'Order', ShippedDate as 'Shipped Date', ShipVia as 'Ship via'
	FROM Orders
	WHERE ShippedDate >= '1998-05-06' AND ShipVia >= 2