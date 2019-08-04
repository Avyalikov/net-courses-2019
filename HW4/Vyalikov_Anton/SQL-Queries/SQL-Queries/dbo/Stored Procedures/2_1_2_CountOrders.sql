CREATE PROCEDURE [dbo].[2_1_2_CountOrders]
AS
	SELECT (COUNT(OrderId) - COUNT(ShippedDate)) as Counter
    FROM Orders
