CREATE PROCEDURE [dbo].[231RegionWestern]
AS
	SELECT DISTINCT CONCAT(LastName, ' ', FirstName) AS Seller
FROM Employees
LEFT JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
LEFT JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
LEFT JOIN Region ON Territories.RegionID = Region.RegionID
WHERE Region.RegionDescription = 'Western'