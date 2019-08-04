CREATE PROCEDURE [dbo].[Procedure_2.2.3]
AS
	SELECT
		(SELECT (LastName + ' ' + FirstName)
		FROM Employees emp 
		WHERE emp.EmployeeID = Orders.EmployeeID) AS 'Seller',
		(SELECT ContactName 
		FROM Customers 
		WHERE Customers.CustomerID = Orders.CustomerID) AS 'Customer',
		COUNT(OrderID) AS 'Amount'
	FROM Orders
	WHERE YEAR(OrderDate) = 1998
	GROUP BY Orders.CustomerID, Orders.EmployeeID

