CREATE PROCEDURE dbo.hw_2_4_3
      
AS   



select CompanyName
from Customers
where not exists(
(SELECT CustomerID
from Orders
where Orders.CustomerID = Customers.CustomerID))


