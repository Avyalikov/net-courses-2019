CREATE PROCEDURE [dbo].[221GroupByYear] 
AS 
BEGIN

SELECT  Year(OrderDate) as 'Year', COUNT(OrderId) as Total
FROM [Orders]
Group by Year(OrderDate)

END
