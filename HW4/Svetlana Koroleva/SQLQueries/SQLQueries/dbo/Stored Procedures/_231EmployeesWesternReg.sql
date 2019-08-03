CREATE PROCEDURE [dbo].[_231EmployeesWesternReg]
AS
	SELECT DISTINCT (e.LastName+' '+e.FirstName) as Employee
    FROM Employees as e JOIN EmployeeTerritories as et
    ON e.EmployeeID=et.EmployeeID
    JOIN Territories as t
    ON et.TerritoryID=t.TerritoryID
    JOIN Region as r
    ON t.RegionID=r.RegionID
    WHERE(r.RegionDescription='Western')