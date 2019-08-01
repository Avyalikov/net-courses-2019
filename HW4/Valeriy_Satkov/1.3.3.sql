USE Northwind;
SELECT CustomerID FROM Customers
WHERE LOWER(LEFT(Country, 1)) >='b' AND LOWER(LEFT(Country, 1)) <='g';
