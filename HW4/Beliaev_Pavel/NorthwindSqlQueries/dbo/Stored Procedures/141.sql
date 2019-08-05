CREATE PROCEDURE [dbo].[141]
AS
	SELECT ProductName
	FROM Products
	WHERE ProductName LIKE '%cho_olade%'
RETURN 0
