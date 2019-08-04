CREATE PROCEDURE [dbo].[123CustomersCountries]
AS
	SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC