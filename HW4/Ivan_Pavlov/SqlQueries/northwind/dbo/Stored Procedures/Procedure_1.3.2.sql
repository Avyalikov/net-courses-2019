CREATE PROCEDURE [dbo].[Procedure_1.3.2]
AS
	SELECT CustomerID, Country
	FROM Customers
	WHERE SUBSTRING(Country, 1, 1) BETWEEN 'b' AND 'g'
	ORDER BY Country

