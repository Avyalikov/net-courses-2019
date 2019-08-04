CREATE PROCEDURE [dbo].[2_3_1_WesternSellers]
AS
	SELECT DISTINCT (emp.LastName + ' ' + emp.FirstName) as Employee
    FROM Employees as emp 
	JOIN EmployeeTerritories as empter
    ON emp.EmployeeID = empter.EmployeeID
    JOIN Territories as ter
    ON empter.TerritoryID = ter.TerritoryID
    JOIN Region as reg
    ON ter.RegionID = reg.RegionID
    WHERE(reg.RegionDescription = 'Western')