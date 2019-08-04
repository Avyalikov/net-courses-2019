create procedure [dbo].[2_3_1]
as
	select
		'Seller' = [LastName] + ' ' + [FirstName]
	from [Employees] as e
		inner join [EmployeeTerritories] as et
			on (e.[EmployeeID] = et.[EmployeeID])
		inner join [Territories] as t
			on et.[TerritoryID] = t.[TerritoryID]
		inner join [Region] as r
			on t.[RegionID] = r.[RegionID]
	where [RegionDescription] = 'Western'
	group by [LastName], [FirstName]
return 0