CREATE PROCEDURE dbo.hw_1_2_2
      
AS   
select substring(ContactName,0,charindex(' ',ContactName)) as ContactName, Country
from Customers
where Country not in ('USA', 'Canada')
order by substring(ContactName,0,charindex(' ',ContactName))

