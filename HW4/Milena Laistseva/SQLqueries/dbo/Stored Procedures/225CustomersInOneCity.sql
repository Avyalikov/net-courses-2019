CREATE PROCEDURE [dbo].[225CustomersInOneCity]
AS
	SELECT
    ContactName AS Customer
FROM Customers
WHERE Customers.City IN 
    (SELECT City
     FROM Customers
     GROUP BY City
     HAVING COUNT(CustomerID)>1)