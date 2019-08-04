create procedure [dbo].[2_2_1]
as
	select
		'Year' = year([OrderDate]),
		'Total' = count([OrderDate])
	from [Orders]
	group by year([OrderDate]);

	--check
	select count([OrderID])
	from [Orders];
return 0