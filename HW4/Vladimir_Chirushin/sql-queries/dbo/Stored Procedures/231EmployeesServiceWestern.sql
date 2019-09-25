-- Task 2.3 #1
CREATE PROCEDURE EmployeesServiceWestern
AS
DECLARE @SearchingRegion nvarchar(100) = 'Western'
SELECT 
	DISTINCT CONCAT(Employees.LastName, ' ', Employees.FirstName) AS 'Seller'
FROM 
	dbo.Employees

LEFT JOIN dbo.EmployeeTerritories ON 
	Employees.EmployeeID = EmployeeTerritories.EmployeeID

LEFT JOIN dbo.Territories ON 
	EmployeeTerritories.TerritoryID = Territories.TerritoryID

LEFT JOIN dbo.Region ON 
	Territories.RegionID = Region.RegionID
WHERE
	Region.RegionDescription = @SearchingRegion
