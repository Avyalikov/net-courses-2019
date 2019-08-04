CREATE PROCEDURE [dbo].[Task1_3_1 Get order IDs with products quantity between 3 and 10 incl]
	@lowerLimit int = 3,
	@upperLimit int = 10
AS
	SELECT DISTINCT OrderID
	FROM [Order Details]
	WHERE Quantity BETWEEN @lowerLimit AND @upperLimit
