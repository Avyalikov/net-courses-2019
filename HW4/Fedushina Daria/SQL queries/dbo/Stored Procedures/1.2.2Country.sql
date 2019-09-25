CREATE PROCEDURE [dbo].[122Country](
	@country1 nvarchar(15),
	@country2 nvarchar(15))
AS 
BEGIN

SELECT [ContactName], [Country]
FROM [dbo].[Customers]
WHERE [Country] NOT IN (@country1 ,
	@country2)
ORDER BY [ContactName] ASC

END

--EXECUTE Northwind.dbo.[122Country] @country1  = 'USA',@country2 = 'Canada'






