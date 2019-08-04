CREATE PROCEDURE [dbo].[1_3_2_CountriesBetweenBG]
AS
	SELECT CustomerID, Country
    FROM Customers
    WHERE Country BETWEEN 'b%' AND 'h%'
    ORDER BY Country
