CREATE PROCEDURE [dbo].[223GroupByEmployee] @year int
AS
BEGIN

SELECT (
SELECT FirstName + LastName 
FROM [Employees]
WHERE [Employees].[EmployeeID] = [Orders].[EmployeeID]) as 'Seller',
(
SELECT [ContactName] 
FROM [Customers]
WHERE [Customers].[CustomerID] = [Orders].[CustomerID]) as 'Byer',
COUNT([orderID]) as 'Amount'
FROM [Orders]
WHERE Year(OrderDate) = @year
GROUP BY [EmployeeID],[CustomerID]
ORDER BY [EmployeeID],[Amount] desc

END

--EXECUTE Northwind.dbo.[223GroupByEmployee] '1998'