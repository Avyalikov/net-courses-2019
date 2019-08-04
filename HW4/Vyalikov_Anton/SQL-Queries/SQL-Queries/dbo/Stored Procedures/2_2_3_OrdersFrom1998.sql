CREATE PROCEDURE [dbo].[2_2_3_OrdersFrom1998]
	@param1 int = 0,
	@param2 int
AS
	SELECT 
	  (SELECT (LastName+' '+FirstName)
	  FROM Employees emp WHERE emp.EmployeeID = ord.EmployeeID) as Seller, 
	     (SELECT ContactName
	     FROM Customers cust WHERE cust.CustomerID = ord.CustomerID) as Customer,
	COUNT(OrderId) as Amount
    FROM Orders as ord
    WHERE YEAR(OrderDate) = 1998
    GROUP BY ord.EmployeeID, ord.CustomerID
