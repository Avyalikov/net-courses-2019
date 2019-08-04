CREATE PROCEDURE [dbo].[2_2_1_Check]
AS
	SELECT COUNT(OrderDate) as 'Total'
	FROM Orders
