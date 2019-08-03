CREATE PROCEDURE [dbo].[_211OrdersSum]
AS
	SELECT SUM(Quantity*UnitPrice*(1-Discount)) as Totals
    FROM [Order Details]
