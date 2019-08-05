CREATE PROCEDURE [dbo].[226]
AS
	SELECT FirstName + ' ' + LastName as Seller,
	(SELECT FirstName + ' ' + LastName FROM Employees WHERE Worker.ReportsTo = Employees.EmployeeID) as Boss
	FROM Employees as Worker
	WHERE ReportsTo IS NOT NULL
RETURN 0
