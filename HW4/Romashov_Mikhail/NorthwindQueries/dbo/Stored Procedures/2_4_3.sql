--Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа 
--(подзапрос по таблице Orders). Использовать оператор EXISTS.

CREATE PROCEDURE [query_2_4_3]
AS

SELECT DISTINCT CustomerID
	FROM Customers
	WHERE NOT EXISTS (SELECT d.CustomerID
					 FROM  Orders as d 
					 WHERE  Customers.CustomerID = d.CustomerID)