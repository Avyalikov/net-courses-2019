-- Task 1.3 #1
SELECT OrderID
FROM dbo.[Order Details]
WHERE Quantity BETWEEN 3 AND 10

-- Task 1.3 #2
SELECT CustomerID, Country
FROM dbo.Customers
WHERE LEFT(Country,1) BETWEEN 'B' AND 'G'
ORDER BY Country

-- Task 1.3 #3
SELECT CustomerID, Country
FROM dbo.Customers
WHERE Country LIKE '[B-G]%'
ORDER BY Country