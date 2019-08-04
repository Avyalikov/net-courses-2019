CREATE PROCEDURE [dbo].[222EmployeeAmountOfOrders]
	@param1 int = 0,
	@param2 int
AS
	SELECT 
      (SELECT CONCAT (LastName, ' ', FirstName) 
         FROM Employees 
         WHERE EmployeeID = Orders.EmployeeID) AS Seller,
       COUNT(EmployeeID) as Amount
FROM Orders
GROUP BY EmployeeID
ORDER BY Amount DESC