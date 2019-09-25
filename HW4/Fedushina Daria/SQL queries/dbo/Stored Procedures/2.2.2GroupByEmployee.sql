CREATE PROCEDURE [dbo].[222GroupByEmployee] 
AS
BEGIN

SELECT (
SELECT FirstName + LastName 
FROM [Employees]
WHERE [Employees].[EmployeeID] = [Orders].[EmployeeID]) as 'Seller',
COUNT([OrderID]) as 'Amount'
FROM [Orders]
GROUP BY [EmployeeID]
ORDER BY [Amount] desc

END
