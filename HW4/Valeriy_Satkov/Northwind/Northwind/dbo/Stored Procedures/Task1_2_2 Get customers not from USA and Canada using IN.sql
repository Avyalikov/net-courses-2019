CREATE PROCEDURE [dbo].[Task1_2_2 Get customers not from USA and Canada using IN]
AS
	SELECT ContactName, Country
	FROM Customers
	WHERE Country NOT IN ('USA', 'Canada')
	ORDER BY ContactName
