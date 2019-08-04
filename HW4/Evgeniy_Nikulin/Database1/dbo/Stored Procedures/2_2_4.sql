create procedure [dbo].[2_2_4]
as
	select
		'Seller' = [LastName] + ' ' + [FirstName],
		'Buyer' = [ContactName],
		[Employees].[City]
	from [Employees], [Customers]
	where [Employees].[City] = [Customers].[City]
return 0