CREATE PROCEDURE [query_2_4_2]
AS	
	SELECT (LastName + ' ' + FirstName)
	FROM Employees
	WHERE (SELECT COUNT(*) 
	FROM Orders 
	WHERE Orders.EmployeeID = Employees.EmployeeID) > 150