CREATE PROCEDURE [dbo].[221OrdersAmountByYears]
AS
	SELECT YEAR(OrderDate) AS Year, COUNT(OrderID) AS Total
FROM Orders
GROUP BY YEAR(OrderDate);
/*checking*/
    SELECT COUNT(OrderDate)
FROM Orders