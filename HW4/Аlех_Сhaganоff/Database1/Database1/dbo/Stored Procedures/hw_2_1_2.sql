CREATE PROCEDURE [dbo].[hw_2_1_2]
      
AS   

select count(OrderID) - count(ShippedDate) as Count
from Orders

