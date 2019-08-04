CREATE PROCEDURE [dbo].[Proc_1_4_1]
AS
	SELECT ProductName
	FROM Products
	WHERE ProductName LIKE '%cho_olade%'
RETURN 0
