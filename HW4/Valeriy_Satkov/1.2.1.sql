USE Northwind;
SELECT ContactName, Country FROM Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY Country, ContactName;
