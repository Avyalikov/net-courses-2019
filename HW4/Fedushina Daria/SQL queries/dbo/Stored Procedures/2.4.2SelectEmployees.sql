CREATE PROCEDURE [dbo].[242SelectEmployees] @ordersCount int
AS
BEGIN

SELECT [Employees].[FirstName] + ' '+ [Employees].[LastName]
FROM [Employees]
WHERE [Employees].[EmployeeID] 
IN  
(SELECT [Orders].[EmployeeID] 
FROM [Orders]
GROUP BY EmployeeID
HAVING COUNT([Orders].[OrderID]) > @ordersCount)
END

--EXECUTE Northwind.dbo.[242SelectEmployees] 150