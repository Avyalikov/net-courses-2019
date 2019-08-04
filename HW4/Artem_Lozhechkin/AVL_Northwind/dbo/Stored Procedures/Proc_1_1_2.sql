CREATE PROCEDURE [dbo].[Proc_1_1_2]
AS
	SELECT OrderId,
	CASE ShippedDate
		WHEN NULL THEN 'Not Shipped'
	END AS ShippedDate
	FROM Orders
	WHERE ShippedDate IS NULL
RETURN 0
