USE Northwind;
SELECT 
   custmrs.CompanyName,
   (SELECT COUNT(ordrs.OrderID) 
    FROM Orders AS ordrs 
	WHERE ordrs.CustomerID = custmrs.CustomerID) AS Amount
FROM Customers AS custmrs
ORDER BY Amount DESC;
