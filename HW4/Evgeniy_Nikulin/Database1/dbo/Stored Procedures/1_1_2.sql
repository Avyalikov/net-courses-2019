create procedure [dbo].[1_1_2]
as
	select 
		[OrderID], 
		[ShippedDate] = 
			case 
				when [ShippedDate] is null then 'Not Shipped'
				else cast([ShippedDate] as char)
			end
	from [Orders]
	where [ShippedDate] is null;
return 0