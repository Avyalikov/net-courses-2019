CREATE PROCEDURE [dbo].[_121CustomersUSACanada]
AS
	SELECT ContactName as Name, Country
    FROM Customers
    WHERE Country IN ('USA', 'Canada')
    ORDER BY Name, Address
