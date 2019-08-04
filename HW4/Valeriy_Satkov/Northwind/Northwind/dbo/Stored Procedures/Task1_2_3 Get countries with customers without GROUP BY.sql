CREATE PROCEDURE [dbo].[Task1_2_3 Get countries with customers without GROUP BY]
AS
	SELECT DISTINCT Country
	FROM Customers
	ORDER BY Country DESC
