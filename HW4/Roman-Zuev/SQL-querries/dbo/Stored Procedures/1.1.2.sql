--Написать запрос, который выводит только недоставленные заказы
--из таблицы Orders. В результатах запроса возвращать для колонки
--ShippedDate вместо значений NULL строку ‘Not Shipped’
--(использовать системную функцию CASЕ). Запрос должен возвращать
--только колонки OrderID и ShippedDate.

CREATE PROCEDURE [1.1.2]
AS

SELECT OrderID,
CASE
WHEN ShippedDate IS NULL
THEN 'Not Shipped'
ELSE CAST(ShippedDate AS CHAR(20))
END ShippedDate
FROM Orders
WHERE ShippedDate IS NULL