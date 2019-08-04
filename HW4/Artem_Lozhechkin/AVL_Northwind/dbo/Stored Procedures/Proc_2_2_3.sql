CREATE PROCEDURE [dbo].[Proc_2_2_3]
AS
	SELECT 
		(SELECT FirstName + ' ' + LastName 
		FROM Employees 
		WHERE Orders.EmployeeID = Employees.EmployeeID) as Seller, 
		(SELECT CompanyName 
		FROM Customers 
		WHERE Orders.CustomerID = Customers.CustomerID) as Customer,
	Count(OrderID) as Amount
	FROM Orders
	WHERE YEAR(OrderDate) = 1998
	GROUP BY EmployeeID, CustomerID
	ORDER BY Amount desc
RETURN 0
