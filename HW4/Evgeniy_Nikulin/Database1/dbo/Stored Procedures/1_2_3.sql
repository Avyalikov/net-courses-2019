create procedure [dbo].[1_2_3]
as
	select distinct [Country]
	from [Customers]
	order by [Country] desc;
return 0