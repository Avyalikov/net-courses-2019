CREATE PROCEDURE [dbo].[Proc_1_1_3]
AS
	SELECT OrderID AS 'Order Number',
	IIF (ShippedDate is NULL, 'Not Shipped', CAST(ShippedDate as nvarchar(10))) AS 'Shipped Date'
	FROM Orders
	WHERE ShippedDate > '1998-05-06' OR ShippedDate is null
RETURN 0
