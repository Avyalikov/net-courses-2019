create procedure [dbo].[2_2_6]
as
	select 
		'Employee' = [LastName] + ' ' + [FirstName],
		'Managers' = (
			select [Managers].[LastName] + ' ' + [Managers].[FirstName]
			from [Employees] as [Managers]
			where [Managers].[EmployeeID] = [Employees].[ReportsTo])
	from [Employees]
return 0