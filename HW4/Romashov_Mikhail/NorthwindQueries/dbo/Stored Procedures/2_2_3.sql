--По таблице Orders найти количество заказов, сделанных каждым продавцом и 
--для каждого покупателя. Необходимо определить это только для заказов, сделанных в 1998 году.

CREATE PROCEDURE [query_2_2_3]
AS

SELECT EmployeeID, CustomerID, COUNT(ShippedDate) as 'Orders'
	FROM Orders
	WHERE YEAR(ShippedDate) = '1998'
	GROUP BY EmployeeID, CustomerID