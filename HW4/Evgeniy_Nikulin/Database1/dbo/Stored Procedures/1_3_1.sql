create procedure [dbo].[1_3_1]
as
	select distinct [OrderID]
	from [Order Details]
	where [Quantity] between 3 and 10;
return 0