create procedure [dbo].[2_1_2]
as
	select count([OrderID]) - count([ShippedDate])
	from [Orders]
return 0