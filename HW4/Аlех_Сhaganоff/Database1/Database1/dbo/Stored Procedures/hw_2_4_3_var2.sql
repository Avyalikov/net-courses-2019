CREATE PROCEDURE dbo.hw_2_4_3_var2
      
AS   



select Customers.CompanyName
from Customers
left outer join Orders
on Customers.CustomerID = Orders.CustomerID
where OrderDate is null
order by OrderDate


