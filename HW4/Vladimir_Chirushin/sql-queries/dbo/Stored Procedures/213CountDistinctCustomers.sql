-- Task 2.1 #3
CREATE PROCEDURE CountDistinctCustomers
AS
SELECT 
	COUNT( DISTINCT CustomerID )
FROM 
	dbo.Orders