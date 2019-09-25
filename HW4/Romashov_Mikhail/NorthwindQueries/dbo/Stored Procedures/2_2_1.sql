--По таблице Orders найти количество заказов с группировкой по годам. 
--В результатах запроса надо возвращать две колонки c названиями Year и Total. 

CREATE PROCEDURE [query_2_2_1]
AS
SELECT YEAR(OrderDate) as 'Year', COUNT(OrderID) as 'Total'
	FROM Orders
	GROUP BY YEAR(OrderDate)

--Написать проверочный запрос, который вычисляет количество всех заказов.
SELECT COUNT(OrderID) as 'Number of orders'
	FROM Orders