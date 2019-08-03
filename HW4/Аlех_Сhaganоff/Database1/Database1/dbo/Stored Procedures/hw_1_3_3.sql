CREATE PROCEDURE dbo.hw_1_3_3
      
AS   
select CustomerID
from Customers
where Left(Country,1) >= 'b' and Left(Country,1) <= 'g'
