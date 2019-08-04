CREATE PROCEDURE [dbo].[1_3_1_OrdersByQuantity]
AS
	SELECT OrderID
    FROM [Order Details]
    WHERE Quantity BETWEEN 3 AND 10
