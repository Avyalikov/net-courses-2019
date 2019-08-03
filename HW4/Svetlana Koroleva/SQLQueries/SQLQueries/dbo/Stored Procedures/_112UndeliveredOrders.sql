CREATE PROCEDURE [dbo].[_112UndeliveredOrders]
AS	
	SELECT OrderID, CASE WHEN ShippedDate IS NULL THEN 'Not Shipped' ELSE CAST(ShippedDate as nvarchar) END as ShippedDate
    FROM Orders
    WHERE ShippedDate IS NULL

