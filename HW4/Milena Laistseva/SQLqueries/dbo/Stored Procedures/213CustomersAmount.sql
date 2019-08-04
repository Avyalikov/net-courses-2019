CREATE PROCEDURE [dbo].[213CustomersAmount]
AS
	SELECT COUNT(DISTINCT CustomerID) as Customers
FROM Orders