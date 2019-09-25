CREATE PROCEDURE [dbo].[_212OrdersUndeliveredAmount]
AS
	SELECT (COUNT(OrderId) - COUNT(ShippedDate)) as Amount
    FROM Orders
