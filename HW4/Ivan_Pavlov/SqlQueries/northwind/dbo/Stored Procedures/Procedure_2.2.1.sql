CREATE PROCEDURE [dbo].[Procedure_2.2.1]
AS
	SELECT YEAR(OrderDate) AS 'Year', COUNt(OrderId) AS 'Total'
	FROM Orders
	GROUP BY YEAR(OrderDate)
