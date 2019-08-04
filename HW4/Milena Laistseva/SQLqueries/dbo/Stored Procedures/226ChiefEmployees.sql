CREATE PROCEDURE [dbo].[226ChiefEmployees]
AS
	 SELECT
 CONCAT (LastName, ' ', FirstName) as Employee,
 (SELECT CONCAT (ChiefEmployees.LastName, ' ', ChiefEmployees.FirstName)
  FROM Employees AS ChiefEmployees
  WHERE ChiefEmployees.EmployeeID = Employees.ReportsTo) AS Chief
 FROM Employees