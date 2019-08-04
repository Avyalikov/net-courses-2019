create procedure [dbo].[1_1_3]
as
	select 
		'Order Number' = [OrderID],
		'Shipped Date' = 	
			case 
				when [ShippedDate] is null then 'Not Shipped'
				else cast([ShippedDate] as char)
			end
	from [Orders]
	where [ShippedDate] > '1998-05-06' or [ShippedDate] is null;
return 0