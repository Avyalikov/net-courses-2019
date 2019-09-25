CREATE PROCEDURE [dbo].[226EmployeeManager]
AS
BEGIN

SELECT [employers].[FirstName] + [employers].[LastName] AS 'Employee', 
(SELECT 
[managers].[FirstName] + [managers].[LastName] 
FROM [Employees] as managers
WHERE [employers].[ReportsTo] = [managers].[EmployeeID]) AS 'Manager'
FROM [Employees] as employers

END