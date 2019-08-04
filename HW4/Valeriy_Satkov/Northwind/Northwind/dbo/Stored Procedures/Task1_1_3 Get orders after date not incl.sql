CREATE PROCEDURE [dbo].[Task1_1_3 Get orders after date not incl]
	@date datetime = '1998-05-06'
AS
	SELECT
		ordrs.OrderID AS 'Order Number',
		'Shipped Date' =
			CASE
				WHEN ordrs.ShippedDate IS NULL THEN 'Not Shipped'
				ELSE CONVERT ( nvarchar , ShippedDate, 21 )
			END
	FROM Orders AS ordrs
	WHERE ordrs.ShippedDate > @date OR ordrs.ShippedDate IS NULL
