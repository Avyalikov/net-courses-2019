CREATE PROCEDURE [dbo].[hw_1_2_3]
      
AS   
select distinct Country
from Customers
where CustomerID is not null
order by Country desc
