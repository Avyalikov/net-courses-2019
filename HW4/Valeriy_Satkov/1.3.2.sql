USE Northwind;
SELECT CustomerID, Country FROM Customers
WHERE
   (LOWER(LEFT(Country, 1)) BETWEEN 'b' AND 'g')
   AND
   EXISTS (SELECT Country FROM Customers WHERE Country = 'Germany')
ORDER BY Country;
