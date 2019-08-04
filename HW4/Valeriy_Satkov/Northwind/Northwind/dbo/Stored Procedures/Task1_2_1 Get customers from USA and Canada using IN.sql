CREATE PROCEDURE [dbo].[Task1_2_1 Get customers from USA and Canada using IN]
AS
	SELECT ContactName, Country
	FROM Customers
	WHERE Country IN ('USA', 'Canada')
	ORDER BY ContactName, Country
