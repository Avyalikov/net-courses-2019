CREATE PROCEDURE [dbo].[Task2_2_5 Get all customers who live in the same city]
AS
	SELECT 
		cstmrs.CompanyName AS 'Customer'
		/*,	cstmrs.City AS 'City'*/
	FROM Customers AS cstmrs
	WHERE cstmrs.City IN (SELECT City
							FROM Customers
							GROUP BY City
							HAVING COUNT(CustomerID) > 1)
	/*ORDER BY 'City'*/
