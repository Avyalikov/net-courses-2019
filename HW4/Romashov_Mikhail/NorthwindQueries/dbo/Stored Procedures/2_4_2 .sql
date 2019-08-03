--Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.

CREATE PROCEDURE [query_2_4_2]
AS

SELECT EmployeeID
	FROM Employees
	WHERE (SELECT COUNT(ShippedDate)  
			FROM Orders 
			WHERE Orders.EmployeeID = Employees.EmployeeID 
			GROUP BY Orders.EmployeeID) > 150