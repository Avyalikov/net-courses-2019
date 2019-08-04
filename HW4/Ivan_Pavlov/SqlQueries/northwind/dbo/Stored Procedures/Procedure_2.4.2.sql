CREATE PROCEDURE [dbo].[Procedure_2.4.2]
AS
	SELECT (LastName + ' ' + FirstName)
	FROM Employees emp
	WHERE emp.EmployeeID IN 
		(SELECT EmployeeID FROM Orders
		GROUP BY EmployeeID
		HAVING COUNT(OrderID) > 150)
