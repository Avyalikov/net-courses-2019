--Выдать в результатах запроса имена всех заказчиков из таблицы Customers и 
--суммарное количество их заказов из таблицы Orders. Принять во внимание, что у некоторых 
--заказчиков нет заказов, но они также должны быть выведены в результатах запроса. 
--Упорядочить результаты запроса по возрастанию количества заказов.

CREATE PROCEDURE [query_2_3_2]
AS

SELECT d.CustomerID as 'Customer', COUNT(o.OrderID) as 'Orders'
	FROM Customers as d
	FULL OUTER JOIN Orders as o on d.CustomerID = o.CustomerID
	GROUP BY d.CustomerID
	ORDER BY COUNT(o.OrderID)