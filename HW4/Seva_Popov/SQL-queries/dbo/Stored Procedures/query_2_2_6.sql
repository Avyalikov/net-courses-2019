CREATE PROCEDURE [dbo].[query_2_2_6]
AS	
	SELECT FirstName + ' ' + LastName as Seller,
	(SELECT FirstName + ' ' + LastName 
	FROM Employees 
	WHERE Worker.ReportsTo = Employees.EmployeeID) as Boss
	FROM Employees as Worker
	WHERE ReportsTo IS NOT NULL
