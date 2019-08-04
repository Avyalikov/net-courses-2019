CREATE PROCEDURE [dbo].[Task2_2_4 Get sellers and custmers who lived in one city without JOIN]
AS
	SELECT
		cstmrs.CompanyName AS 'Person'  
		--, cstmrs.City AS 'City', 'WhoIs' = 'Customer'
	FROM Customers AS cstmrs
	WHERE cstmrs.City IN (SELECT City FROM Employees)
	UNION
	SELECT
		CONCAT(empls.LastName, ' ', empls.FirstName) AS 'Person'  
		--, empls.City AS 'City', 'WhoIs' = 'Seller'
	FROM Employees AS empls
	WHERE empls.City IN (SELECT City FROM Customers)
	--ORDER BY 'City'
