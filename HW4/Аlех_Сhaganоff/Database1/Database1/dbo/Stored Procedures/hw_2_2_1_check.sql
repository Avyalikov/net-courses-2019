CREATE PROCEDURE dbo.hw_2_2_1_check
      
AS   

select count(OrderID)
from Orders
