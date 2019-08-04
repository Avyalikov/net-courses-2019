USE Northwind;
SELECT
   (SELECT CONCAT( employs.LastName, ' ', employs.FirstName)
    FROM Employees AS employs
	WHERE employs.EmployeeID = ordrs.EmployeeID) AS Seller,
   (SELECT custmrs.CompanyName
    FROM Customers AS custmrs
	WHERE custmrs.CustomerID = ordrs.CustomerID) AS Customer,
   COUNT(ordrs.OrderID) AS Ammount
FROM Orders AS ordrs
WHERE YEAR(ordrs.OrderDate)=1998
GROUP BY ordrs.EmployeeID, ordrs.CustomerID
ORDER BY Seller, Customer;
