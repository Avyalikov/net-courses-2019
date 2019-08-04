CREATE PROCEDURE [dbo].[242SellersWithMoreThan150Orders]
AS
	SELECT CONCAT(LastName, ' ', FirstName) AS Seller
FROM Employees
WHERE Employees.EmployeeID IN 
                           (SELECT EmployeeID 
                            FROM Orders 
							GROUP BY EmployeeID 
							HAVING COUNT(OrderID)>150)