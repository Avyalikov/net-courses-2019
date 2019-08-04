create procedure [dbo].[1_2_2]
as
	select [ContactName], [Country]
	from [Customers]
	where [Country] not in ('USA', 'Canada')
	order by [ContactName];
return 0