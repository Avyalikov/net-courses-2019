CREATE PROCEDURE [dbo].[_123CustomersCountries]
AS
	SELECT DISTINCT Country
    FROM Customers
    WHERE CustomerID IS NOT NULL
    ORDER BY Country DESC
