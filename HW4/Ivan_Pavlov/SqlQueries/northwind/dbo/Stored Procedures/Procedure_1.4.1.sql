CREATE PROCEDURE [dbo].[Procedure_1.4.1]
AS
	SELECT ProductName
	FROM Products
	WHERE ProductName LIKE '%cho_olade%';
