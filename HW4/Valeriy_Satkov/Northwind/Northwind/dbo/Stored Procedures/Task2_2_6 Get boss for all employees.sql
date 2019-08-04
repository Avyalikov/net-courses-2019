CREATE PROCEDURE [dbo].[Task2_2_6 Get boss for all employees]
AS
	SELECT
		CONCAT(empls.LastName, ' ', empls.FirstName) AS 'Seller',
		(SELECT CONCAT(LastName, ' ', FirstName)
			FROM Employees
			WHERE EmployeeID = empls.ReportsTo) AS 'Boss'
	FROM Employees as empls
