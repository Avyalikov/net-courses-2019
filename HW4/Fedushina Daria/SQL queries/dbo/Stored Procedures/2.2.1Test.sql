CREATE PROCEDURE [dbo].[221Test]
AS
SELECT 
	COUNT(OrderDate) As 'Total'
FROM [Orders]