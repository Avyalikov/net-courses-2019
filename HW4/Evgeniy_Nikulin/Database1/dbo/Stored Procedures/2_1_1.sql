create procedure [dbo].[2_1_1]
as
	select 'Totals' = sum([UnitPrice] *[Quantity] * (1 - [Discount]) )
	from [Order Details];
return 0