CREATE PROCEDURE [dbo].[212UndeliveriedOrdersAmount]
AS
	SELECT (COUNT(*) - COUNT(ShippedDate)) AS 'Undeliveried orders'
FROM Orders