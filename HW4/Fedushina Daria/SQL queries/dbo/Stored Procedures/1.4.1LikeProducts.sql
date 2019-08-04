CREATE PROCEDURE [dbo].[141LikeProducts]
AS 
BEGIN

SELECT [ProductName]
FROM [dbo].[Products]
WHERE  [ProductName] like '%cho_olade%'

END




