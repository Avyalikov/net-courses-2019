CREATE PROCEDURE [dbo].[hw_2_2_2]
      
AS   


select
(select Employees.FirstName + ' ' + Employees.LastName
from Employees
where Employees.EmployeeID = Orders.EmployeeID) as "Seller", count(orderID) as "Amount"
from Orders
group by Orders.EmployeeID
order by Amount desc
