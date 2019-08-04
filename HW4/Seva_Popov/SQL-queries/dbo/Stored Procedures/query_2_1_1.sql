CREATE PROCEDURE [query_2_1_1]
AS
	SELECT SUM(Quantity * UnitPrice * (1 - Discount)) as 'Totals'
	FROM [Order Details]
