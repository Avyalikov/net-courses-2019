CREATE PROCEDURE [dbo].[Proc_2_3_1]
AS
	SELECT DISTINCT FirstName + ' ' + LastName as Employee
	FROM Employees
	JOIN EmployeeTerritories ON EmployeeTerritories.EmployeeID = Employees.EmployeeID
	JOIN Territories ON Territories.TerritoryID = EmployeeTerritories.TerritoryID
	JOIN Region ON Territories.RegionID = Region.RegionID
	WHERE RegionDescription = 'Western'
RETURN 0
