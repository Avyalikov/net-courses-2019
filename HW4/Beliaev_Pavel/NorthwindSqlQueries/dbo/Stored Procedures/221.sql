CREATE PROCEDURE [dbo].[221]
AS
	SELECT YEAR(OrderDate) as 'Year', COUNT(*) as 'Total'
	FROM Orders
	GROUP BY YEAR(OrderDate)
RETURN 0
