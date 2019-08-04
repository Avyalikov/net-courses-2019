--Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается 
--на буквы из диапазона b и g, не используя оператор BETWEEN.

CREATE PROCEDURE [query_1_3_3]
AS

SELECT CustomerID as 'Customer'
	FROM Customers 
	WHERE LEFT(Country,1) >= 'b' AND LEFT(Country,1) <= 'g'