--По таблице Orders найти количество заказов, которые еще не были доставлены 
--(т.е. в колонке ShippedDate нет значения даты доставки). Использовать при этом 
--запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.

CREATE PROCEDURE [query_2_1_2]
AS

SELECT COUNT(*) - COUNT(ShippedDate) as 'Number of orders not shipped'
	FROM Orders