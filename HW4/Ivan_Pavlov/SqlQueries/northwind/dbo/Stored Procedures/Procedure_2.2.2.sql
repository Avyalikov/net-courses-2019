CREATE PROCEDURE [dbo].[Procedure_2.2.2]
AS
	SELECT
		(SELECT (LastName + ' ' + FirstName) 
		FROM Employees emp
		WHERE emp.EmployeeID = Orders.EmployeeID) AS 'Seller',
		COUNT(OrderID) AS 'Amount'
	FROM Orders
	GROUP BY EmployeeID
	ORDER BY Amount DESC
