--По таблице Employees найти для каждого продавца его руководителя

CREATE PROCEDURE [query_2_2_6]
AS

SELECT DISTINCT e.EmployeeID, e.ReportsTo
	FROM Employees as e, Employees as d
	WHERE e.ReportsTo = d.ReportsTo