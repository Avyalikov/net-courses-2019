--Определить продавцов, которые обслуживают регион 'Western' (таблица Region)

CREATE PROCEDURE [query_2_3_1]
AS

SELECT DISTINCT Employees.FirstName as 'Name'
	FROM Employees
	INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
	INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
	INNER JOIN Region ON Region.RegionID = Territories.RegionID
	WHERE Region.RegionDescription = 'Western'