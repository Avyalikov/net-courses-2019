CREATE PROCEDURE [dbo].[_223OrdersEmpCustomers]
AS
	SELECT 
	     (SELECT (LastName+' '+FirstName)
	     FROM Employees e WHERE e.EmployeeID=o.EmployeeID ) as Seller, 
	     (SELECT ContactName
	     FROM Customers c WHERE c.CustomerID=o.CustomerID ) as Customer,
	     COUNT(OrderId) as Amount
    FROM Orders as o
    WHERE YEAR(OrderDate) =1998
    GROUP BY o.EmployeeID, o.CustomerID