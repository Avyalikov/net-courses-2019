CREATE PROCEDURE [dbo].[211OrdersSumWithDiscount]
AS
	SELECT SUM(UnitPrice*Quantity*(1-Discount)) AS Totals
FROM [Order Details]