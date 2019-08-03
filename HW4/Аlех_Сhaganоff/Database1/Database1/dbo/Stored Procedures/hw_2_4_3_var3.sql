CREATE PROCEDURE dbo.hw_2_4_3_var3
      
AS   



select CompanyName
from Customers
join 
(select distinct CustomerID
from Customers
Except
select distinct CustomerID
from Orders) as c on  Customers.CustomerID = c.CustomerID


