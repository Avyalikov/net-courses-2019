CREATE PROCEDURE [dbo].[Procedure_1.1.2]
AS
	SELECT Orders.OrderID, 
	CASE
	WHEN Orders.ShippedDate IS NULL
	THEN 'Not Shipped'
	ELSE CAST(ShippedDate AS NVARCHAR)
	END ShippedDate
	FROM Orders
	WHERE ShippedDate IS NULL


