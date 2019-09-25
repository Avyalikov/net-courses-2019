--По таблице Orders найти количество различных покупателей (CustomerID),
--сделавших заказы. Использовать функцию COUNT и не использовать 
--предложения WHERE и GROUP.

CREATE PROCEDURE [2.1.3]
AS

SELECT COUNT(DISTINCT CustomerID)
FROM Orders