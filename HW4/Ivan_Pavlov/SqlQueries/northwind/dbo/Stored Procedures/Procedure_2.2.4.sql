CREATE PROCEDURE [dbo].[Procedure_2.2.4]
AS
	SELECT ContactName AS 'Name'
	FROM Customers c, Employees emp
	WHERE c.City = emp.City
	UNION 
	SELECT (LastName + ' ' + FirstName)
	FROM Employees emp, Customers c
	WHERE emp.City = c.City
