CREATE PROCEDURE [dbo].[132CustomersCountriesBG]
AS
	SELECT CustomerID, Country
FROM Customers
WHERE LEFT(Country, 1) BETWEEN 'B' AND 'G'
ORDER BY Country
