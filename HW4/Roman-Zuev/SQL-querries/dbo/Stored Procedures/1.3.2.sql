--Выбрать всех заказчиков из таблицы Customers, у которых 
--название страны начинается на буквы из диапазона b и g. 
--Использовать оператор BETWEEN. Проверить, что в результаты 
--запроса попадает Germany. Запрос должен возвращать только колонки 
--CustomerID и Country и отсортирован по Country.

CREATE PROCEDURE [1.3.2]
AS

SELECT CustomerID, Country
FROM Customers
WHERE LEFT (Country,1) BETWEEN 'b' AND 'g'
ORDER BY Country