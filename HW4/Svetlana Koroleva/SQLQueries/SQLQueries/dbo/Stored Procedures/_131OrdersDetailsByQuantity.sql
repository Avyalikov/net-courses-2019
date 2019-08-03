CREATE PROCEDURE [dbo].[_131OrdersDetailsByQuantity]
AS
	SELECT DISTINCT OrderID
    FROM [Order Details]
    WHERE Quantity BETWEEN 3 AND 10
