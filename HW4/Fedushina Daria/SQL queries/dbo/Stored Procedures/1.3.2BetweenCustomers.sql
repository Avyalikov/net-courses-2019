CREATE PROCEDURE [dbo].[132BetweenCustomers](
	@char1 char(1),
	@char2 char(1))
AS 
BEGIN

SELECT [CustomerID], [Country]
FROM [dbo].[Customers]
WHERE  Left([Country],1) BETWEEN @char1 AND @char2 
Order By [Country]

END

--EXECUTE Northwind.dbo.[131BetweenCustomers] @char1  = 'b',@char2 = 'g'










