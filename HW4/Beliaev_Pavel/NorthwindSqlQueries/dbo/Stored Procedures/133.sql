CREATE PROCEDURE [dbo].[133]
AS
	SELECT CustomerID, Country
	FROM [Customers]
	WHERE Lower(Left(Country, 1)) >= 'b' AND Lower(Left(Country, 1)) < 'h'
RETURN 0
