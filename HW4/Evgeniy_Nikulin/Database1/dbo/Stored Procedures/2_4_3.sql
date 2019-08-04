create procedure [dbo].[2_4_3]
as
	select
		[CompanyName],
		[ContactName]
	from [Customers]
	where not exists(
		select *
		from [Orders]
		where [Customers].[CustomerID] = [Orders].[CustomerID])
return 0