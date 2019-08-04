CREATE PROCEDURE [dbo].[Task1_3_3 Get customers which countries start from b to g without using BETWEEN]
AS
	SELECT CustomerID
	FROM Customers
	WHERE LOWER(LEFT(Country, 1)) >='b'
		AND LOWER(LEFT(Country, 1)) <='g'
