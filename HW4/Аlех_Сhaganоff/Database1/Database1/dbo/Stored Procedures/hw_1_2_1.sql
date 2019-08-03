CREATE PROCEDURE [dbo].[hw_1_2_1]
      
AS   
select ContactName, Country
from Customers
where Country in ('USA', 'Canada')
order by substring(ContactName,0,charindex(' ',ContactName)), Country

