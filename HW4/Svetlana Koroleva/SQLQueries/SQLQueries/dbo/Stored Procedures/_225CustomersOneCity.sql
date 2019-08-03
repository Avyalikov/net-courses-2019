CREATE PROCEDURE [dbo].[_225CustomersOneCity]
AS
	SELECT c.ContactName
    FROM Customers as c
    WHERE c.City IN
       (SELECT City
       FROM Customers
       GROUP BY City
       HAVING COUNT(CustomerId)>1)