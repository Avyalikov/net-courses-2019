CREATE PROCEDURE [dbo].[223EmploeesAndCustomersOrdersIn1998]
AS
	SELECT
(SELECT CONCAT (LastName, ' ', FirstName) FROM Employees WHERE EmployeeID = Orders.EmployeeID) AS Sellers,
(SELECT ContactName FROM Customers WHERE CustomerID = Orders.CustomerID) AS Customers, 
COUNT(OrderID) AS Amount
FROM Orders
WHERE YEAR(OrderDate) = '1998'
GROUP BY EmployeeID, CustomerID