CREATE PROCEDURE [dbo].[FEDoRDER3.1] AS 
SELECT DISTINCT [Country] 
FROM [dbo].[Customers]
ORDER BY [Country] DESC
RETURN







