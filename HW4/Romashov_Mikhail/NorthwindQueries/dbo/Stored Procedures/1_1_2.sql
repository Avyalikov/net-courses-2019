--Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
--В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку 
--‘Not Shipped’ (использовать системную функцию CASЕ). Запрос должен возвращать только 
--колонки OrderID и ShippedDate.

CREATE PROCEDURE [query_1_1_2]
AS

SELECT OrderID,
	CASE
		WHEN ShippedDate IS NULL
		THEN 'Not Shipped'
	END ShippedDate
	FROM Orders
	WHERE ShippedDate IS NULL