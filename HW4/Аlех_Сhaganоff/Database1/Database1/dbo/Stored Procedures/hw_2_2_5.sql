CREATE PROCEDURE dbo.hw_2_2_5
      
AS   


select ContactName
from Customers
where Customers.City in  
(select City
from Customers
group by City
having count(CustomerID)>1)
