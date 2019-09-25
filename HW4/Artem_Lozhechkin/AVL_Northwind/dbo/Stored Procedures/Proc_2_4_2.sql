CREATE PROCEDURE [dbo].[Proc_2_4_2]
AS
	SELECT FirstName + ' ' + LastName as 'Top Sellers'
	FROM Employees
	WHERE (SELECT COUNT(*) FROM Orders WHERE Orders.EmployeeID = Employees.EmployeeID) > 150
RETURN 0
