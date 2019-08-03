--По таблице Orders найти количество заказов, сделанных каждым продавцом
--и для каждого покупателя. Необходимо определить это только для заказов,
--сделанных в 1998 году.

CREATE PROCEDURE [2.2.3]
AS

SELECT EmployeeID, CustomerID, COUNT (OrderID) AS 'Amount'
FROM ORDERS 
WHERE YEAR(OrderDate) = '1998'
GROUP BY EmployeeID, CustomerID