--Найти общую сумму всех заказов из таблицы Order Details с учетом количества 
--закупленных товаров и скидок по ним. Результатом запроса должна быть одна 
--запись с одной колонкой с названием колонки 'Totals'.

CREATE PROCEDURE [query_2_1_1]
AS

SELECT SUM(UnitPrice * Quantity *
		CASE Discount
			WHEN 0 THEN 1 --Если ноль, то умноаем на 1
			ELSE 1-Discount --Если скидка, то умноаем на процент
		END
	) as 'Totals'
	FROM [Order Details]