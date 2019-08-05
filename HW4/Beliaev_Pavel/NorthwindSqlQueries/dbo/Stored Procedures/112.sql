CREATE PROCEDURE [dbo].[112]
AS
	SELECT OrderId,
	CASE ShippedDate
		WHEN NULL THEN 'Not Shipped'
	END AS ShippedDate
	FROM Orders
	WHERE ShippedDate IS NULL
RETURN 0
