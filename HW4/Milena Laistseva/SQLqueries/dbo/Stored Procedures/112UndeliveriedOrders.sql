CREATE PROCEDURE [dbo].[112UndeliveriedOrders]
AS
    SELECT  OrderID,
    CASE 
    WHEN ShippedDate IS NULL
    THEN 'Not Shipped'
    END ShippedDate
 FROM Orders
 WHERE ShippedDate IS NULL
