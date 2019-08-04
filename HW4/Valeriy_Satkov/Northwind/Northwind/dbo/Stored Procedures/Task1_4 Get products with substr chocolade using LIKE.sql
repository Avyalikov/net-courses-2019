CREATE PROCEDURE [dbo].[Task1_4 Get products with substr chocolade using LIKE]
AS
	SELECT ProductName
	FROM Products
	WHERE ProductName LIKE '%cho_olade%'
