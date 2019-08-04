-- Task 2.2 #3
CREATE PROCEDURE OrdersByEmployeeIn1998
AS
DECLARE @OrderDate datetime = '1998'
SELECT
	(SELECT 
		CONCAT(Employees.LastName, ' ', Employees.FirstName)
	FROM
		dbo.Employees
	WHERE
		Employees.EmployeeID = Orders.EmployeeID) AS 'Seller',
	
	(SELECT
		Customers.ContactName
	FROM
		dbo.Customers
	WHERE 
		Customers.CustomerID = Orders.CustomerID) AS 'Customer',
		COUNT(OrderID) AS 'Amount'
FROM
	dbo.Orders
WHERE 
	YEAR(Orders.OrderDate) =  YEAR(CAST(@OrderDate AS DATE)) 
GROUP BY
	Orders.EmployeeID, Orders.CustomerID
