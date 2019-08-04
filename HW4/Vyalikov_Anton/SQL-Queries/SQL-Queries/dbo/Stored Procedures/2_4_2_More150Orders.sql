CREATE PROCEDURE [dbo].[2_4_2_More150Orders]
AS
	SELECT (emp.LastName + emp.FirstName) as Employee
    FROM Employees as emp
    WHERE emp.EmployeeID IN 
	  (SELECT EmployeeID 
      FROM Orders as ord
      GROUP BY EmployeeID
      HAVING COUNT(ord.OrderID) > 150)