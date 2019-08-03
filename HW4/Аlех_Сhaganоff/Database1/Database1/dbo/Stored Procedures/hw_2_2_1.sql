CREATE PROCEDURE dbo.hw_2_2_1
      
AS   

select Year(OrderDate) as "Year", Count(distinct OrderID) as Total
from Orders
group by Year(OrderDate)
