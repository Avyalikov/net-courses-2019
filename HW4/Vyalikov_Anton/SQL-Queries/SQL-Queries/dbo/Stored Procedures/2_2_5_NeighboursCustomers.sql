CREATE PROCEDURE [dbo].[2_2_5_NeighboursCustomers]
AS
	SELECT cust.ContactName
    FROM Customers as cust
    WHERE cust.City IN
       (SELECT City
       FROM Customers
       GROUP BY City
       HAVING COUNT(CustomerId) > 1)