CREATE PROCEDURE [dbo].[Proc_1_3_2]
AS
	SELECT CustomerID, Country
	FROM [Customers]
	WHERE Country BETWEEN 'b' AND 'h'
	ORDER BY Country
RETURN 0
