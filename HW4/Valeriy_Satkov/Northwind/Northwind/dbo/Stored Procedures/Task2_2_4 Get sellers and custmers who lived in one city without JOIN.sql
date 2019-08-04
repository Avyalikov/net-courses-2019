CREATE PROCEDURE [dbo].[Task2_2_4 Get sellers and custmers who lived in one city without JOIN]
AS
	SELECT
		(SELECT CONCAT( employs.LastName, ' ', employs.FirstName)
			FROM Employees AS employs
			WHERE employs.EmployeeID = ordrs.EmployeeID) AS Seller,
		(SELECT custmrs.CompanyName
			FROM Customers AS custmrs
			WHERE custmrs.CustomerID = ordrs.CustomerID) AS Customer,
		ordrs.ShipCity AS 'City'
	FROM Orders AS ordrs, Employees AS employs
	WHERE 
		ordrs.ShipCity = employs.City AND 
		ordrs.ShipCountry = employs.Country AND
		(ordrs.ShipRegion = employs.Region OR ordrs.ShipRegion IS NULL)
	GROUP BY ordrs.ShipCity, ordrs.EmployeeID, ordrs.CustomerID
	ORDER BY 'City', Seller, Customer
