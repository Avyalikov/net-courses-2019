CREATE PROCEDURE [dbo].[Task1_3_2 Get customers which countries start from b to g using BETWEEN]
AS
	SELECT CustomerID, Country
	FROM Customers
	WHERE
		(LOWER(LEFT(Country, 1)) BETWEEN 'b' AND 'g')
		AND
		EXISTS (SELECT Country
				FROM Customers
				WHERE Country = 'Germany')
	ORDER BY Country
