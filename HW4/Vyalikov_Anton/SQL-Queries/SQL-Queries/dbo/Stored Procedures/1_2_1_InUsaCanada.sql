CREATE PROCEDURE [dbo].[1_2_1_InUsaCanada]
AS
	SELECT ContactName, Country
    FROM Customers
    WHERE Country IN ('USA', 'Canada')
    ORDER BY ContactName, Address
