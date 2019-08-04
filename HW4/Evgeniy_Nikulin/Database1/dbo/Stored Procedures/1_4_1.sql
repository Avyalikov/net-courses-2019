create procedure [dbo].[1_4_1]
as
	select distinct [ProductName]
	from [Products]
	where [ProductName] like '%cho%olade%';
return 0