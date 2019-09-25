CREATE PROCEDURE [dbo].[Task2_2_1 Get quantity of orders with GROUPing BY year]
AS
	SELECT
		YEAR(ordrs.OrderDate) AS 'Year',
		COUNT(ordrs.OrderDate) AS Total
	FROM Orders AS ordrs
	GROUP BY YEAR(ordrs.OrderDate)
