CREATE PROCEDURE [dbo].[131BetweenOrderDetails](
	@int1 int,
	@int2 int)
AS 
BEGIN

SELECT DISTINCT [OrderID]
FROM [dbo].[Order Details]
WHERE [Quantity] BETWEEN @int1 AND @int2

END

--EXECUTE Northwind.dbo.[131BetweenOrderDetails] @int1  = '3',@int2 = '10'
















