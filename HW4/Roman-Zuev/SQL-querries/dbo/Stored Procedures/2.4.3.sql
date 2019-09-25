--Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа
--(подзапрос по таблице Orders). Использовать оператор EXISTS.

CREATE PROCEDURE [2.4.3]
AS

SELECT CompanyName FROM Customers t1
WHERE NOT EXISTS (SELECT CustomerID FROM Orders t2 WHERE t1.CustomerID = t2.CustomerID)