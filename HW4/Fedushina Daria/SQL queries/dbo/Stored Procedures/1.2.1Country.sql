CREATE PROCEDURE [dbo].[121Country](
	@country1 nvarchar(15),
	@country2 nvarchar(15))
AS 
BEGIN

SELECT [ContactName], [Country]
FROM [dbo].[Customers]
WHERE [Country] IN (@country1 ,
	@country2)
ORDER BY [ContactName],[Country] ASC

END

--EXECUTE Northwind.dbo.121Country @country1  = 'USA',@country2 = 'Canada'