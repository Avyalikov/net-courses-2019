CREATE PROCEDURE [dbo].[_221OrdersAmountByYear]
AS
	SELECT YEAR(OrderDate) as Year, COUNT(OrderID) as Total
    FROM Orders
    GROUP BY YEAR(OrderDate)

