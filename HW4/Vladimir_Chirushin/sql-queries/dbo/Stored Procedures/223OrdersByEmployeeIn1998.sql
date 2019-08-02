-- Task 2.2 #3
CREATE PROCEDURE OrdersByEmployeeIn1998
AS
DECLARE @OrderDateLimit datetime = '1998'
SELECT 
	CONCAT(Employees.FirstName, ' ', Employees.LastName) AS 'Seller', 
	Customers.ContactName AS 'Customer'
FROM 
	dbo.Employees, 
	dbo.Orders, 
	dbo.Customers
WHERE 
	Employees.EmployeeID = Orders.EmployeeID AND 
	Orders.CustomerID = Customers.CustomerID AND 
	Orders.OrderDate = CAST(@OrderDateLimit as date) 
GROUP BY 
	Customers.ContactName , 
	CONCAT(Employees.FirstName, ' ', Employees.LastName)
ORDER BY 
	'Seller'