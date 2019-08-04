--По таблице Orders найти количество различных покупателей (CustomerID), 
--сделавших заказы. Использовать функцию COUNT и не использовать предложения WHERE и GROUP.

CREATE PROCEDURE [query_2_1_3]
AS

SELECT COUNT(DISTINCT CustomerID) as 'Number of customers'
	FROM Orders