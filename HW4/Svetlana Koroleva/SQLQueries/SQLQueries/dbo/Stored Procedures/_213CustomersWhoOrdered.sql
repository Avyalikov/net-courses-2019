CREATE PROCEDURE [dbo].[_213CustomersWhoOrdered]
AS
	SELECT COUNT(DISTINCT CustomerID) as Amount
    FROM Orders
