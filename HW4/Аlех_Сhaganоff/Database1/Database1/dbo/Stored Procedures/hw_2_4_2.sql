CREATE PROCEDURE dbo.hw_2_4_2
      
AS   



select FirstName + ' ' + LastName as "Employee"
from Employees
where EmployeeID in
(select EmployeeID
from Orders
group by EmployeeID
having count(OrderID)>150)


