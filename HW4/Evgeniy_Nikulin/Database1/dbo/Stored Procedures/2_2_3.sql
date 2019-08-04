create procedure [dbo].[2_2_3]
as
	select
		'Seller' = (
			select [LastName] + ' ' + [FirstName]
			from [Employees] 
			where [EmployeeID] = [Orders].[EmployeeID]),
		'Buyer' = (
			select [ContactName]
			from [Customers] 
			where [CustomerID] = [Orders].[CustomerID]),
		'Count' = count([CustomerID])
	from [Orders]
	where year([OrderDate]) = '1998'
	group by [EmployeeID], [CustomerID]
	order by [Seller];
return 0