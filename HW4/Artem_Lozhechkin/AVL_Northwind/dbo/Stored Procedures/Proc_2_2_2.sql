CREATE PROCEDURE [dbo].[Proc_2_2_2]
AS
	SELECT (SELECT LastName + ' ' + FirstName 
	FROM Employees 
	WHERE Orders.EmployeeID = Employees.EmployeeID) as Seller, Count(OrderID) as Amount
	FROM Orders
	GROUP BY EmployeeID
	ORDER BY Amount desc
RETURN 0
