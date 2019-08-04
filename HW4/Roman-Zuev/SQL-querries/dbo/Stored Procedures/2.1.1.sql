--Найти общую сумму всех заказов из таблицы Order Details с учетом 
--количества закупленных товаров и скидок по ним. Результатом запроса 
--должна быть одна запись с одной колонкой с названием колонки 'Totals'.

CREATE PROCEDURE [2.1.1]
AS

SELECT SUM( (UnitPrice * (1-Discount))*Quantity ) AS 'Totals'
FROM [Order Details]