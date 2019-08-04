--По таблице Orders найти количество заказов с группировкой по годам.
--В результатах запроса надо возвращать две колонки c названиями Year и Total.
--Написать проверочный запрос, который вычисляет количество всех заказов.

CREATE PROCEDURE [2.2.1]
AS

SELECT year(OrderDate) AS 'Year', COUNT (OrderID) AS 'Total'
FROM Orders
GROUP BY year(OrderDate)

SELECT COUNT(OrderID) AS 'Orders Amount' FROM Orders