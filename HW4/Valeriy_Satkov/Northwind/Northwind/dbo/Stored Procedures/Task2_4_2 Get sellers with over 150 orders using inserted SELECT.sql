CREATE PROCEDURE [dbo].[Task2_4_2 Get sellers with over 150 orders using inserted SELECT]
AS
	SELECT
		(SELECT CONCAT(empls.LastName, ' ', empls.FirstName)
			FROM Employees AS empls
			WHERE empls.EmployeeID = ordrs.EmployeeID) AS Seller
	FROM Orders As ordrs
	GROUP BY ordrs.EmployeeID
	HAVING COUNT(ordrs.OrderID) > 150
