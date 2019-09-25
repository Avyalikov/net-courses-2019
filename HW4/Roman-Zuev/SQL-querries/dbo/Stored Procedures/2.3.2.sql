--Выдать в результатах запроса имена всех заказчиков из таблицы Customers
--и суммарное количество их заказов из таблицы Orders. Принять во внимание,
--что у некоторых заказчиков нет заказов, но они также должны быть выведены в
--результатах запроса. Упорядочить результаты запроса по возрастанию количества заказов.

CREATE PROCEDURE [2.3.2]
AS

SELECT t1.CompanyName, COUNT(t2.OrderID) AS 'Orders Amount'
FROM Customers t1
LEFT JOIN Orders t2 ON t2.CustomerID = t1.CustomerID
GROUP BY t1.CompanyName
ORDER BY COUNT(t2.OrderID)