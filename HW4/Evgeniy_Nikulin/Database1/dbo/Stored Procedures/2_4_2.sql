create procedure [dbo].[2_4_2]
as
	select 'Seller' = [LastName] + ' ' + [FirstName]
	from [Employees]
	where [EmployeeID] in (
		select [EmployeeID]
		from [Orders]
		group by [EmployeeID]
		having count([OrderID]) > 150)
return 0