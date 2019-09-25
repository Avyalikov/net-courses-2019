CREATE PROCEDURE [query_2_2_3]
AS	
	SELECT
	(SELECT (LastName + ' ' + FirstName)
	FROM Employees emp 
	WHERE emp.EmployeeID = Orders.EmployeeID) as 'Seller',
	(SELECT ContactName 
	FROM Customers 
	WHERE Customers.CustomerID = Orders.CustomerID) as 'Customer',
	COUNT(OrderID) as 'Amount'
	FROM Orders
	WHERE YEAR(OrderDate) = 1998
	GROUP BY Orders.CustomerID, Orders.EmployeeID
