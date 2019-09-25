CREATE PROCEDURE [dbo].[212CountOrders]
AS 
BEGIN

SELECT COUNT([OrderID]) - COUNT([ShippedDate]) AS Anshipped
FROM [Orders]

END




