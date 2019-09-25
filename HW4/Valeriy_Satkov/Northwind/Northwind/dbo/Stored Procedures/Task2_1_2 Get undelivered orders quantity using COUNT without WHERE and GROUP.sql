CREATE PROCEDURE [dbo].[Task2_1_2 Get undelivered orders quantity using COUNT without WHERE and GROUP]
AS
	SELECT COUNT(*) - COUNT(ShippedDate)
	FROM Orders
