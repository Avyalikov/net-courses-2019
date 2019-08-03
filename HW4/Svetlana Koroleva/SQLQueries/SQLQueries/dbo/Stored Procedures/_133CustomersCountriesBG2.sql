CREATE PROCEDURE [dbo].[_133CustomersCountriesBG2]
AS
	SELECT CustomerID, Country
    FROM Customers
    WHERE Country LIKE '[b-g]%'
    ORDER BY Country
