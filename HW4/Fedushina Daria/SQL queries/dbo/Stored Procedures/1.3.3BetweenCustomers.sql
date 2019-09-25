CREATE PROCEDURE [dbo].[133BetweenCustomers](
	@char1 char(1),
	@char2 char(1))
AS 
BEGIN

SELECT [CustomerID], [Country]
FROM [dbo].[Customers]
WHERE  Left([Country],1)>=@char1 AND Left([Country],1)<=@char2
ORDER BY [Country]


END

--EXECUTE Northwind.dbo.[133BetweenCustomers] @char1  = 'b',@char2 = 'g'










