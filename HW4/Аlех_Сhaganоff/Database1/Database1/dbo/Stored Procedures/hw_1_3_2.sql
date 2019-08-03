CREATE PROCEDURE dbo.hw_1_3_2
      
AS   
select CustomerID, Country
from Customers
where Left(Country,1) between 'b' and 'g'
order by Country
