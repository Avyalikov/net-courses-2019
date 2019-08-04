--Определить продавцов, которые обслуживают регион 'Western' (таблица Region).

CREATE PROCEDURE [2.3.1]
AS

SELECT DISTINCT t1.LastName, t1.FirstName, t4.RegionDescription
FROM Employees t1 
LEFT JOIN EmployeeTerritories t2 ON 
t1.EmployeeID = t2.EmployeeID

LEFT JOIN Territories t3 ON 
t2.TerritoryID = t3.TerritoryID

LEFT JOIN Region t4 ON 
t3.RegionID = t4.RegionID

WHERE t4.RegionDescription = 'Western'