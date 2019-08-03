CREATE PROCEDURE dbo.hw_1_1_2
      
AS   
select OrderID, 
case when ShippedDate is null then 'Not Shipped' else cast(ShippedDate as varchar) end as ShippedDate
from Orders
where ShippedDate is null
