CREATE PROCEDURE [dbo].[Task2_1_1 Get sum of orders based on quantity and discounts]
AS
	SELECT SUM(UnitPrice * Quantity * (1 - Discount)) AS Totals
	FROM [Order Details]
