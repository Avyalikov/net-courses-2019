CREATE PROCEDURE [dbo].[224CustomerAndEmployeeSameCity]
AS
	SELECT
CONCAT (Employees.LastName, ' ', Employees.FirstName) AS Seller,
Customers.ContactName AS Customer
FROM Employees, Customers
WHERE Employees.City = Customers.City
