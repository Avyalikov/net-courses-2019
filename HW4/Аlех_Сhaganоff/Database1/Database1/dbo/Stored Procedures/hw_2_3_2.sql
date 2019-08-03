CREATE PROCEDURE dbo.hw_2_3_2
      
AS   


select ContactName, count(OrderID) as "Number of orders"
from Customers
left outer join Orders on Customers.CustomerID = Orders.CustomerID
group by ContactName
order by "Number of orders" asc

