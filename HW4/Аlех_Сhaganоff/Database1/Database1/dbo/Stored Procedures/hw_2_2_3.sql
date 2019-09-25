CREATE PROCEDURE dbo.hw_2_2_3
      
AS   


select
(select Employees.FirstName + ' ' + Employees.LastName
from Employees
where Employees.EmployeeID = Orders.EmployeeID) as "Employee", 
(select Customers.ContactName
from Customers
where Customers.CustomerID = Orders.CustomerID) as "Customer", count(OrderID) as "Orders"
from orders
where Year(OrderDate) = 1998
group by EmployeeID, CustomerID
order by EmployeeID, CustomerID
