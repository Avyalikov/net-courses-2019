CREATE PROCEDURE [query_2_3_1]
AS	
	SELECT  DISTINCT(LastName +' '+ FirstName) as Employee
	FROM Employees  
	JOIN EmployeeTerritories 
	ON EmployeeTerritories.EmployeeID = Employees.EmployeeID
	JOIN Territories 
	ON Territories.TerritoryID = EmployeeTerritories.TerritoryID
	JOIN Region 
	ON Territories.RegionID = Region.RegionID
	WHERE RegionDescription = 'Western'