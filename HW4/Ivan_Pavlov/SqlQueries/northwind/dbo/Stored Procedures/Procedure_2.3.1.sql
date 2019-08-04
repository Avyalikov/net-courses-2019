CREATE PROCEDURE [dbo].[Procedure_2.3.1]
AS
	SELECT  DISTINCT(LastName + ' ' + FirstName) AS 'Employee Name'
	FROM Employees emp JOIN EmployeeTerritories et ON emp.EmployeeID = et.EmployeeID
	JOIN Territories t ON t.TerritoryID = et.TerritoryID
	JOIN Region r ON r.RegionID = t.RegionID
	WHERE r.RegionDescription = 'Western'
