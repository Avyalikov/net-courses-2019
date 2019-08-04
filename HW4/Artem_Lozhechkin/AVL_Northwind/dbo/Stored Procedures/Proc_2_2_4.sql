CREATE PROCEDURE [dbo].[Proc_2_2_4]
AS
	SELECT DISTINCT
		(SELECT FirstName + ' ' + LastName 
		FROM Employees 
		WHERE Orders.EmployeeID = Employees.EmployeeID) as Seller, 
		(SELECT CompanyName
		FROM Customers 
		WHERE Orders.CustomerID = Customers.CustomerID) as Customer,
		ShipCity as City
	FROM Orders, Customers, Employees
	WHERE Employees.City = Customers.City AND Customers.City = Orders.ShipCity
	ORDER BY 'City', Seller, Customer
RETURN 0
