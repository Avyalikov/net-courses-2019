USE Northwind;
SELECT DISTINCT CONCAT(employs.LastName, ' ', employs.FirstName) AS Seller
FROM Employees AS employs
INNER JOIN EmployeeTerritories AS employTerr
   ON employTerr.EmployeeID = employs.EmployeeID
INNER JOIN Territories AS terr
   ON terr.TerritoryID = employTerr.TerritoryID
INNER JOIN Region AS reg
   ON reg.RegionID = terr.RegionID
WHERE reg.RegionDescription = 'Western'
ORDER BY Seller;
