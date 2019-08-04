--Выбрать всех заказчиков из таблицы Customers, у которых название 
--страны начинается на буквы из диапазона b и g, не используя оператор BETWEEN.

CREATE PROCEDURE [1.3.3]
AS

SELECT CustomerID, Country
FROM Customers
WHERE LEFT (Country,1) >='b' AND LEFT (Country,1) <='g'
ORDER BY Country
