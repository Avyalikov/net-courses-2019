--По таблице Employees найти для каждого продавца его руководителя.

CREATE PROCEDURE [2.2.6]
AS

SELECT t1.LastName, t1.FirstName, t1.Title, Concat(t2.LastName, ' ', t2.FirstName) AS 'Supervisor', t2.Title as 'Supervisor title'
FROM Employees t1, Employees t2
WHERE t1.ReportsTo = t2.EmployeeID