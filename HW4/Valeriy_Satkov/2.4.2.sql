USE Northwind;
SELECT
   (SELECT CONCAT(empls.LastName, ' ', empls.FirstName)
    FROM Employees AS empls
	WHERE empls.EmployeeID = ordrs.EmployeeID) AS Seller/*,
   COUNT(ordrs.OrderID) AS Amount */
FROM Orders As ordrs
GROUP BY ordrs.EmployeeID
HAVING COUNT(ordrs.OrderID) > 150;
