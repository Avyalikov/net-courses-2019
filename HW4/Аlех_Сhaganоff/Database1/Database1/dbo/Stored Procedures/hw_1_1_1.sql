CREATE PROCEDURE dbo.hw_1_1_1
      
AS   
select OrderID, ShippedDate, ShipVia
from Orders
where ShippedDate >= '1998-05-06' and ShipVia >= 2 
