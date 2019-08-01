USE Northwind;
SELECT ContactName, Country FROM Customers
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY Country, ContactName;
