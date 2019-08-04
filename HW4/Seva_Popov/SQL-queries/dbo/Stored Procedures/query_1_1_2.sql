CREATE PROCEDURE [dbo].[query_1_1_2]
AS

	SELECT OrderID,
	CASE
	WHEN ShippedDate IS NULL
	THEN 'Not Shipped'
	ELSE CAST(ShippedDate as NVARCHAR)
	END ShippedDate
	FROM Orders
	WHERE ShippedDate IS NULL
