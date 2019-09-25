CREATE PROCEDURE [dbo].[Proc_2_1_1]
AS
	SELECT SUM(UnitPrice * Quantity * (1 - Discount)) AS Totals
	FROM [Order Details]
RETURN 0
