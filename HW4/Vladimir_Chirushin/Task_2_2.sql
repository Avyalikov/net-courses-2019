-- Task 2.2 #1
SELECT YEAR(OrderDate) AS 'Year', Count(OrderDate) as 'Total'
FROM dbo.Orders 
GROUP BY YEAR(OrderDate)


SELECT COUNT(OrderDate) As 'Total'
FROM dbo.Orders 


-- Task 2.2 #2 
SELECT CONCAT(Employees.FirstName, ' ', Employees.LastName) AS 'Seller', COUNT(Orders.EmployeeID) AS  'Amount'
FROM dbo.Employees, dbo.Orders
WHERE Employees.EmployeeID = Orders.EmployeeID
GROUP BY Orders.EmployeeID, CONCAT(Employees.FirstName, ' ', Employees.LastName)
ORDER BY 'Amount' DESC


-- Check for
-- SELECT Orders.EmployeeID, COUNT(Orders.OrderID)
-- FROM dbo.Orders
-- GROUP BY Orders.EmployeeID

-- Task 2.2 #3
