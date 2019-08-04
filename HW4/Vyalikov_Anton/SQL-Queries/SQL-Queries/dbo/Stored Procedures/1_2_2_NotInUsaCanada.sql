CREATE PROCEDURE [dbo].[1_2_2_NotInUsaCanada]
AS
	SELECT ContactName, Country
    FROM Customers
    WHERE Country NOT IN ('USA', 'Canada')
    ORDER BY ContactName, Address
