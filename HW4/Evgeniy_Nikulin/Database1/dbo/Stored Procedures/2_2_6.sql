create procedure [dbo].[2_2_6]
as
	select 
		'Employee' = [LastName] + ' ' + [FirstName],
		'Menagers' = (
			select [Menagers].[LastName] + ' ' + [Menagers].[FirstName]
			from [Employees] as [Menagers]
			where [Menagers].[EmployeeID] = [Employees].[ReportsTo])
	from [Employees]
return 0