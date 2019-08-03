CREATE PROCEDURE [dbo].[_222OrdersEmployees]
AS
	SELECT 
	 (SELECT (LastName+' '+FirstName)
	  FROM Employees e WHERE e.EmployeeID=o.EmployeeID ) as Seller, COUNT(OrderId) as Amount
    FROM Orders as o
    GROUP BY o.EmployeeID
    ORDER BY Amount DESC
