CREATE PROCEDURE [dbo].[222]
AS
	SELECT (SELECT LastName + ' ' + FirstName 
	FROM Employees 
	WHERE Orders.EmployeeID = Employees.EmployeeID) as Seller, Count(OrderID) as Amount
	FROM Orders
	GROUP BY EmployeeID
	ORDER BY Amount desc
RETURN 0
