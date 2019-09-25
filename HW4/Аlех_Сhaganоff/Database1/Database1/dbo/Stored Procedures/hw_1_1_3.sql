CREATE PROCEDURE dbo.hw_1_1_3
      
AS   
select OrderID as "Order Number", 
case when ShippedDate is null then 'Not Shipped' else cast(ShippedDate as varchar) end as "Shipped Date"
from Orders
where ShippedDate > '1998-05-06' or ShippedDate is null
