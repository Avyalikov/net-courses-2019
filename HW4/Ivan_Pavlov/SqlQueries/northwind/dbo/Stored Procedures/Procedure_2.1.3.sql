CREATE PROCEDURE [dbo].[Procedure_2.1.3]
AS
	SELECT COUNT(DISTINCT CustomerID) AS 'Customers'
	FROM Orders
