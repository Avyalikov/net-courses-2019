CREATE PROCEDURE [dbo].[1_1_2_NotShippedOrders]
AS
	SELECT OrderID, 
	CASE WHEN ShippedDate IS NULL THEN 'Not Shipped' 
	ELSE CAST(ShippedDate as nvarchar) END as ShippedDate
    FROM Orders
    WHERE ShippedDate IS NULL
