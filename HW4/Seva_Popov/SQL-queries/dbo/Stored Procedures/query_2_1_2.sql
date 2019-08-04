CREATE PROCEDURE [query_2_1_2]
AS	
	SELECT COUNT(*) - COUNT(ShippedDate)
	FROM Orders
