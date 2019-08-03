CREATE PROCEDURE dbo.hw_2_1_3
      
AS   

select count(distinct CustomerID) as "Count"
from Orders

