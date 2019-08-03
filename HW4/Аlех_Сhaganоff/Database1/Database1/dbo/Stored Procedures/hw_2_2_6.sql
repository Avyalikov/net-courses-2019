CREATE PROCEDURE dbo.hw_2_2_6
      
AS   


select e1.FirstName + ' ' + e1.LastName as "Employee",
(select e2.FirstName + ' ' + e2.LastName
from Employees as e2
where e1.ReportsTo = e2.EmployeeID) as "Manager"
from Employees as e1
