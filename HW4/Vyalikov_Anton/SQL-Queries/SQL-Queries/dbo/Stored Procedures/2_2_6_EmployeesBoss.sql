CREATE PROCEDURE [dbo].[2_2_6_EmployeesBoss]
AS
	SELECT (emp.LastName + ' ' + emp.FirstName) as Employee,
    (boss.LastName + ' ' + boss.FirstName) as Boss
    FROM Employees as emp 
	LEFT JOIN Employees as boss
    ON emp.ReportsTo = boss.EmployeeID