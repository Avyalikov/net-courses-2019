CREATE PROCEDURE [dbo].[113DeliveriedByDateOrUndeliveriedOrders]
AS
	SELECT OrderID AS 'Order Number',
CASE WHEN ShippedDate IS NULL
THEN 'Not Shipped'
ELSE CAST (ShippedDate AS CHAR(20))
END  AS 'Shipped Date'
FROM Orders
WHERE ShippedDate IS NULL
OR ShippedDate > '1998-05-06'
