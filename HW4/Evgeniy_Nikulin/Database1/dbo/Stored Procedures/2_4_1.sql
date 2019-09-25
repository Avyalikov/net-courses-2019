create procedure [dbo].[2_4_1]
as
	select [CompanyName]
	from [Suppliers]
	where [SupplierID] in (
		select [SupplierID]
		from [Products]
		where [UnitsInStock] = 0);
return 0