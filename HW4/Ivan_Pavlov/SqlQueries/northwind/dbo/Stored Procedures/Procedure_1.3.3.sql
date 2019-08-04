CREATE PROCEDURE [dbo].[Procedure_1.3.3]
AS
	SELECT CustomerID, Country
	FROM Customers
	WHERE SUBSTRING(Country, 1, 1) >= 'b' AND SUBSTRING(Country, 1, 1) <= 'g'
	ORDER BY Country

