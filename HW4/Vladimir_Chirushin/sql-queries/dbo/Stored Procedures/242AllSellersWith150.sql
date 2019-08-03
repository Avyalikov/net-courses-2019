-- Task 2.4 #2
CREATE PROCEDURE AllSellersWith150
AS
SELECT
	CONCAT(Employees.FirstName, ' ', Employees.LastName) AS 'Seller'
FROM
	dbo.Employees
WHERE
	Employees.EmployeeID IN
		(SELECT
			Orders.EmployeeID
		FROM
			dbo.Orders
		GROUP BY
			Orders.EmployeeID
		HAVING 
			COUNT(Orders.OrderID)>150)