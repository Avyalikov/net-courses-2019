CREATE PROCEDURE [dbo].[224CustomerAndEmployeeSameCity]
AS
	SELECT Customers.ContactName AS Customer
FROM Employees, Customers
WHERE Employees.City = Customers.City
UNION
SELECT CONCAT (Employees.LastName, ' ', Employees.FirstName) AS Seller
FROM Employees, Customers
WHERE Employees.City = Customers.City
