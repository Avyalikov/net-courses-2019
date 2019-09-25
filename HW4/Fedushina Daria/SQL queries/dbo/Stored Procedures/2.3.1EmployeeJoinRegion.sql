CREATE PROCEDURE [dbo].[231EmployeeJoinRegion]   @region nchar(50)
AS
BEGIN

SELECT DISTINCT [Employees].[FirstName] + [Employees].[LastName] AS 'Employee', [Region].[RegionDescription] AS 'Region'
FROM 
	[Employees]
	JOIN [EmployeeTerritories]
	ON [Employees].[EmployeeID] = [EmployeeTerritories].[EmployeeID]
	JOIN [Territories]
	ON [Territories].[TerritoryID]=[EmployeeTerritories].[TerritoryID]
	JOIN [Region]
	ON [Region].[RegionID] = [Territories].[RegionID]
WHERE [Region].[RegionDescription] = @region

END


--EXECUTE Northwind.dbo.[231EmployeeJoinRegion] 'Western'
