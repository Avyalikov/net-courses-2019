CREATE PROCEDURE [dbo].[Procedure_2.2.1.Check]
AS
	SELECT COUNT(OrderDate) As 'Total'
	FROM Orders 