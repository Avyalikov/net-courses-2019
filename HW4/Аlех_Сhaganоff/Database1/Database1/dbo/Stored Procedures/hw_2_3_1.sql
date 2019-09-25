CREATE PROCEDURE dbo.hw_2_3_1
      
AS   


select distinct FirstName + ' ' + LastName as "Employee"
from Employees
join EmployeeTerritories on Employees.EmployeeID = EmployeeTerritories.EmployeeID
join Territories on EmployeeTerritories.TerritoryID = Territories.TerritoryID
join Region on Territories.RegionID = Region.RegionID
where Region.RegionDescription = 'Western'
