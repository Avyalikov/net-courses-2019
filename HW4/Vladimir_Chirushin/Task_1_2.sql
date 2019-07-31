-- Task 1.2 #1
SELECT ContactName, Country
FROM dbo.Customers
WHERE
Country NOT IN ('USA', 'Canada')
GROUP BY ContactName, Country

-- Task 1.2 #2
SELECT ContactName, MAX(Country) as 'Country'
FROM dbo.Customers
WHERE
Country NOT IN ('USA', 'Canada')
GROUP BY ContactName

-- Task 1.2 #3
SELECT DISTINCT Country 
FROM dbo.Customers
ORDER BY Country DESC
