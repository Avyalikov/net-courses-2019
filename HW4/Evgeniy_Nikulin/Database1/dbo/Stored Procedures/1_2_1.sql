create procedure [dbo].[1_2_1]
as
	select [ContactName], [Country]
	from [Customers]
	where [Country] in ('USA', 'Canada')
	order by [ContactName], [Country];
return 0