CREATE PROCEDURE [dbo].[132]
AS
	SELECT CustomerID, Country
	FROM [Customers]
	WHERE Country BETWEEN 'b' AND 'h'
	ORDER BY Country
RETURN 0
