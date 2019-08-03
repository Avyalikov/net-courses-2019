CREATE PROCEDURE [dbo].[Procedure_2.1.2]
AS
	SELECT COUNT(OrderID) - COUNT(ShippedDate) as 'Expired'
	FROM Orders
