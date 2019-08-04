CREATE PROCEDURE [dbo].[Proc_2_2_5]
AS
	SELECT CompanyName, City
	FROM Customers as cust
	WHERE (SELECT COUNT(1) 
			FROM Customers 
			WHERE cust.City = Customers.City 
			GROUP BY City) > 1
RETURN 0
