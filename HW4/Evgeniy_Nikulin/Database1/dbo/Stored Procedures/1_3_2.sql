create procedure [dbo].[1_3_2]
as
	select distinct [CustomerID], [Country]
	from [Customers]
	where LEFT([Country],1) between 'b' and 'g'
	order by [Country];
return 0