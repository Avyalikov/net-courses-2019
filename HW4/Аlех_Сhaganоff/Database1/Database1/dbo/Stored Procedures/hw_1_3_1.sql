CREATE PROCEDURE dbo.hw_1_3_1
      
AS   
select distinct OrderID
from "Order Details"
where Quantity between 3 and 10
