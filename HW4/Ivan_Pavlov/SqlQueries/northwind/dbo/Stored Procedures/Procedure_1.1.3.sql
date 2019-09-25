CREATE PROCEDURE [dbo].[Procedure_1.1.3]
	AS
	SELECT 
		OrderID AS 'Order Number',
		CASE 
			WHEN ShippedDate IS NULL 
			THEN 'Not shipped'
			ELSE CONVERT(NVARCHAR, ShippedDate, 0)
		 END AS 'Shipped Date'
	FROM Orders
	WHERE ShippedDate > '1998-05-06' OR ShippedDate IS NULL;