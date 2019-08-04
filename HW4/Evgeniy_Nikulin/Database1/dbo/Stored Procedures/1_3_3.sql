create procedure [dbo].[1_3_3]
as
	select distinct [CustomerID], [Country]
	from [Customers]
	where 'b' <= LEFT([Country], 1) and LEFT([Country],1) <= 'g';
return 0