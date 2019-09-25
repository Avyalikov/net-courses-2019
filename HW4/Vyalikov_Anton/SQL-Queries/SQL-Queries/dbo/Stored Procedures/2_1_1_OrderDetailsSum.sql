CREATE PROCEDURE [dbo].[2_1_1_OrderDetailsSum]
AS
	SELECT SUM(Quantity * UnitPrice * (1-Discount)) as Totals
    FROM [Order Details]