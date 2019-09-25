CREATE PROCEDURE [dbo].[1_3_3_CountriesBG]
AS
	SELECT CustomerID, Country
    FROM Customers
    WHERE Country LIKE '[b-g]%'
    ORDER BY Country