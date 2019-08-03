CREATE PROCEDURE [dbo].[hw_2_2_4]
      
AS   


select
(select e.FirstName + ' ' + e.LastName) as "Employees and Cutsomers"
from Employees as e, Customers as c
where e.City = c.City
union
select c.ContactName
from Employees as e, Customers as c
where e.City = c.City
