CREATE PROCEDURE [dbo].[211]
AS
	SELECT SUM(UnitPrice * Quantity * (1 - Discount)) AS Totals
	FROM [Order Details]
RETURN 0
