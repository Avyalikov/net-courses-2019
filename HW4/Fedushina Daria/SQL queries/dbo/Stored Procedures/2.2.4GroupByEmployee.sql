CREATE PROCEDURE [dbo].[224GroupByEmployee]
AS
BEGIN

SELECT [Employees].[FirstName] + [Employees].[LastName] AS 'Seller', [Customers].[ContactName] AS 'Byer'
FROM [Employees], [Customers]
WHERE [Employees].[City] = [Customers].[City]

ORDER BY [EmployeeID]

END
