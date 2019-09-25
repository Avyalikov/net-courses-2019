create procedure [dbo].[2_2_5]
as
	select 
		[ContactName],
		[City]
	from [Customers]
	where [City] in (
		select [City]
		from [Customers]
		group by [City]
		having count([City]) > 1)
	group by [City], [ContactName]
return 0