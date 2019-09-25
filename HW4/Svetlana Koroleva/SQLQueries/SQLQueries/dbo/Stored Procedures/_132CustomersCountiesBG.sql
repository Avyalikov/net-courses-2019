CREATE PROCEDURE [dbo].[_132CustomersCountiesBG]
AS
	SELECT CustomerID, Country
    FROM Customers
    WHERE Country BETWEEN  'b%' AND  'h%'
    ORDER BY Country
