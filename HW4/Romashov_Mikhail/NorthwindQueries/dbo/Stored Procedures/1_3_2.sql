--Выбрать всех заказчиков из таблицы Customers, у которых 
--название страны начинается на буквы из диапазона b и g. Использовать оператор BETWEEN. 
--Проверить, что в результаты запроса попадает Germany. 
--Запрос должен возвращать только колонки CustomerID и Country и отсортирован по Country.

CREATE PROCEDURE [query_1_3_2]
AS

SELECT CustomerID as 'Customer', Country
	FROM Customers 
	WHERE LEFT(Country,1) BETWEEN 'b' AND 'g'
	ORDER BY Country