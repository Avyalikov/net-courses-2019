USE Northwind;
SELECT YEAR(ordrs.OrderDate) AS Year, COUNT(ordrs.OrderDate) AS Total
FROM Orders AS ordrs
GROUP BY YEAR(ordrs.OrderDate);
