CREATE PROCEDURE [dbo].[Task2_2_1 Get quantity orders]
AS
	SELECT COUNT(OrderID) as 'Quantity of orders' -- Check query
	FROM Orders
