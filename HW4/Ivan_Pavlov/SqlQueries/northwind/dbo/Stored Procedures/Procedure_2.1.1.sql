CREATE PROCEDURE [dbo].[Procedure_2.1.1]
AS
	SELECT SUM(Quantity * UnitPrice * (1 - Discount)) AS 'Totals'
	FROM [Order Details]
