-- Task 2.2 #2
CREATE PROCEDURE OrdersByEmployee
AS
SELECT 
	CONCAT(Employees.FirstName, ' ', Employees.LastName) AS 'Seller', 
	COUNT(Orders.EmployeeID) AS  'Amount'
FROM 
	dbo.Employees, 
	dbo.Orders
WHERE 
	Employees.EmployeeID = Orders.EmployeeID
GROUP BY 
	Orders.EmployeeID, 
	CONCAT(Employees.FirstName, ' ', Employees.LastName)
ORDER BY 
	'Amount' DESC