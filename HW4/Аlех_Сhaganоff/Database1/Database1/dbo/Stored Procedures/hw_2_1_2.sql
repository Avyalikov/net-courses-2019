CREATE PROCEDURE [dbo].[hw_2_1_2]
      
AS   

select count(OrderDate) - count(ShippedDate) as Count
from Orders

