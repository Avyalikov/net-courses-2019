CREATE PROCEDURE [dbo].[2_2_1_TotalYear]
AS
	SELECT YEAR(OrderDate) as Year, COUNT(OrderID) as Total
    FROM Orders
    GROUP BY YEAR(OrderDate)
