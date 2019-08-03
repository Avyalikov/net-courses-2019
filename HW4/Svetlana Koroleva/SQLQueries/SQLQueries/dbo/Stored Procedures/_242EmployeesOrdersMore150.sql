CREATE PROCEDURE [dbo].[_242EmployeesOrdersMore150]
AS
	SELECT (e.LastName+e.FirstName) as Employee
    FROM Employees as e
    WHERE e.EmployeeID IN (SELECT EmployeeID 
    FROM Orders as o
    GROUP BY EmployeeID
    HAVING COUNT(o.OrderID)>150)