create procedure [dbo].[1_1_1]
as
	select [OrderID], [ShippedDate], [ShipVia]
	from [Orders]
	where [ShippedDate] >= '1998-05-06' and [ShipVia] >= 2;
return 0