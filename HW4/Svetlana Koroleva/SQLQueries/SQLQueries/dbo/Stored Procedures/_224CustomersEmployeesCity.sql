CREATE PROCEDURE [dbo].[_224CustomersEmployeesCity]
AS
	SELECT c.ContactName as Name
    FROM Customers as c, Employees as e
    WHERE c.City=e.City
    UNION
    SELECT (e.LastName+e.FirstName)
    FROM Customers as c, Employees as e
    WHERE c.City=e.City