create procedure [dbo].[2_2_2]
as
	select 
		'Seller' = (
			select [LastName] + ' ' + [FirstName]
			from [Employees] 
			where [EmployeeID] = [Orders].[EmployeeID]),
		'Amount' = count([EmployeeID])
	from [Orders]
	group by [EmployeeID]
	order by [Amount] desc;
return 0