-- Task 2.2 #1
CREATE PROCEDURE OrdersByYears
AS
SELECT 
	YEAR(OrderDate) AS 'Year', 
	Count(OrderDate) as 'Total'
FROM 
	dbo.Orders
GROUP BY
	YEAR(OrderDate)
ORDER BY 
	'Year' DESC