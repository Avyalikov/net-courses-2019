--Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
--Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
--Не использовать предложение GROUP BY. Возвращать только одну колонку в результатах запроса.

CREATE PROCEDURE [query_1_2_3]
AS

SELECT DISTINCT Country
	FROM Customers
	WHERE CustomerID IS NOT NULL
	ORDER BY Country DESC