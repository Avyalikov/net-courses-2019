CREATE PROCEDURE fe AS 
SELECT [ContactName], [Country]
FROM [dbo].[Customers]
WHERE [Country] in ('USA','Canada')
ORDER BY [ContactName],[Country] ASC