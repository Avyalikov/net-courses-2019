CREATE PROCEDURE [dbo].[hw_2_1_1]
      
AS   

select sum(Quantity*(UnitPrice-UnitPrice*Discount)) as Totals
from "Order Details"
