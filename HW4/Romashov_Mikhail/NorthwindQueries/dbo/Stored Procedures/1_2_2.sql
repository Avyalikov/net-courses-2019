--Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
--Запрос сделать с помощью оператора IN. Возвращать колонки с именем пользователя и 
--названием страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков.

CREATE PROCEDURE [query_1_2_2]
AS

SELECT ContactName as 'Name', Country
	FROM Customers
	WHERE Country NOT IN ('USA', 'Canada')
	ORDER BY ContactName