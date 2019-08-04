CREATE PROCEDURE [dbo].[Task1_1_2 Get undelivered orders]
AS
	SELECT OrderID, ShippedDate =
		CASE
			WHEN ShippedDate IS NULL THEN 'Not Shipped'  
		END
	FROM Orders
	WHERE ShippedDate IS NULL
