CREATE PROCEDURE dbo.hw_1_3_3_var2
      
AS   
select CustomerID, Country
from Customers
where Country Like '[b-g]%'
