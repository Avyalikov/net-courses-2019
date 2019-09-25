CREATE PROCEDURE [dbo].[2_1_3_CountCustomerID]
AS
	SELECT COUNT(DISTINCT CustomerID) as Counter
    FROM Orders
