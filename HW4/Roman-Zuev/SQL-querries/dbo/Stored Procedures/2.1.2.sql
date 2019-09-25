--По таблице Orders найти количество заказов, которые еще не были доставлены 
--(т.е. в колонке ShippedDate нет значения даты доставки). Использовать при
--этом запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.

CREATE PROCEDURE [2.1.2]
AS

SELECT COUNT (*) - COUNT (ShippedDate)
FROM Orders