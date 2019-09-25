create procedure [dbo].[2_3_2]
as
	select
		'Buyer' = c.[ContactName],
		'Orders quantity' = count(o.[OrderID])
	from [Customers] as c
		left join [Orders] as o
			on (c.[CustomerID] = o.[CustomerID])
	group by c.[ContactName]
	order by [Orders quantity]
return 0