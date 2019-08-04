USE Northwind;
SELECT 
   (SELECT CONCAT( employs.LastName, ' ', employs.FirstName )
    FROM Employees AS employs
	WHERE employs.EmployeeID = ordrs.EmployeeID) AS Seller, 
   COUNT(ordrs.OrderID) AS Ammount
FROM Orders AS ordrs
GROUP BY ordrs.EmployeeID
ORDER BY Ammount DESC;
