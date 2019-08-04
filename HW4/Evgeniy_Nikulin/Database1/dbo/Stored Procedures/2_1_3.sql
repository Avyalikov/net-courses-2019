create procedure [dbo].[2_1_3]
as
	select count(distinct [CustomerID])
	from [Orders];
return 0