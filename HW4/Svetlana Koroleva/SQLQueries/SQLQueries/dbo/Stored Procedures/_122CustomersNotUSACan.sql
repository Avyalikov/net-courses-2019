CREATE PROCEDURE [dbo].[_122CustomersNotUSACan]
AS
	SELECT ContactName as Name, Country
    FROM Customers
    WHERE Country NOT IN ('USA', 'Canada')
    ORDER BY Name, Address
