CREATE PROCEDURE [dbo].[2_2_2_EmployeesOrdersAmount]
AS
	SELECT 
	  (SELECT (LastName + ' ' + FirstName)
	  FROM Employees emp WHERE emp.EmployeeID = ord.EmployeeID ) as Seller, COUNT(OrderId) as Amount
    FROM Orders as ord
    GROUP BY ord.EmployeeID
    ORDER BY Amount DESC
