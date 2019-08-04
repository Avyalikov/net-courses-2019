CREATE PROCEDURE [dbo].[123Country] 
AS
BEGIN

SELECT DISTINCT [Country] 
FROM [dbo].[Customers]
ORDER BY [Country] DESC

END




