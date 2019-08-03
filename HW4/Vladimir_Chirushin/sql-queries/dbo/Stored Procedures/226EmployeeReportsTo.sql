-- Task 2.2 #6
CREATE PROCEDURE EmployeeReportsTo
AS
SELECT
	CONCAT(Employees.FirstName, ' ', Employees.LastName) AS 'Seller', 
	(SELECT
		CONCAT(ManagerEmployees.FirstName, ' ', ManagerEmployees.LastName)
	FROM 
		dbo.Employees AS ManagerEmployees
	WHERE
		ManagerEmployees.EmployeeID = Employees.ReportsTo) AS 'Reports To'
FROM
	dbo.Employees