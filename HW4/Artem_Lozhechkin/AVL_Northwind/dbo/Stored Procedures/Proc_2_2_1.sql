CREATE PROCEDURE [dbo].[Proc_2_2_1]
AS
	SELECT YEAR(OrderDate) as 'Year', COUNT(*) as 'Total'
	FROM Orders
	GROUP BY YEAR(OrderDate)
RETURN 0
