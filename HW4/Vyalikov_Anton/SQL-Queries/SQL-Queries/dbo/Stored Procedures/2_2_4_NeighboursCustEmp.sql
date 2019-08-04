CREATE PROCEDURE [dbo].[2_2_4_NeighboursCustEmp]
AS
	SELECT cust.ContactName
    FROM Customers as cust, Employees as emp
    WHERE cust.City = emp.City
    UNION
    SELECT (emp.LastName + emp.FirstName)
    FROM Customers as cust, Employees as emp
    WHERE cust.City = emp.City
