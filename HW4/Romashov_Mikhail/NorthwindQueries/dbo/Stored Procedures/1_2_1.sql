--Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
--Запрос сделать с только помощью оператора IN. Возвращать колонки с именем пользователя 
--и названием страны в результатах запроса. Упорядочить результаты запроса по имени 
--заказчиков и по месту проживания.

CREATE PROCEDURE [query_1_2_1]
AS

SELECT ContactName as 'Name', Country
	FROM Customers
	WHERE Country IN ('USA', 'Canada')
	ORDER BY Country, ContactName