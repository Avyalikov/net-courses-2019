CREATE PROCEDURE [dbo].[Procedure1]
AS
	SELECT (emp2.LastName + ' ' + emp2.FirstName) AS 'Employee Name',
		   (emp1.LastName + ' ' + emp1.FirstName) AS 'Boss Name' 
	FROM Employees emp1 RIGHT JOIN Employees emp2 
	ON emp1.EmployeeID = emp2.ReportsTo
	ORDER BY [Employee Name]

