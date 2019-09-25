CREATE PROCEDURE [dbo].[FEDoRDER2] AS 
SELECT [ContactName], [Country]
FROM [dbo].[Customers]
WHERE [Country] not in ('USA','Canada')
ORDER BY [ContactName] ASC






