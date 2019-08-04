CREATE PROCEDURE [dbo].[133CustomersCountriesWithoutBetween]
AS
	SELECT CustomerID, Country
FROM Customers
WHERE Country >='B' AND Country < 'H'
ORDER BY Country