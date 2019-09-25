CREATE PROCEDURE [query_2_2_1]
AS	
	SELECT YEAR(OrderDate) as 'Year', Count(OrderDate) as 'Total'
	FROM Orders
	GROUP BY YEAR(OrderDate)

