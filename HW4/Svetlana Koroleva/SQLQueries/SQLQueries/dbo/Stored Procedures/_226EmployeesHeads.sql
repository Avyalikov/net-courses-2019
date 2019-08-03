CREATE PROCEDURE [dbo].[_226EmployeesHeads]
AS
	SELECT (e.LastName+' '+e.FirstName) as Employee,
    (h.LastName+' '+h.FirstName) as Head
    FROM Employees as e LEFT JOIN Employees as h
    ON e.ReportsTo=h.EmployeeID
