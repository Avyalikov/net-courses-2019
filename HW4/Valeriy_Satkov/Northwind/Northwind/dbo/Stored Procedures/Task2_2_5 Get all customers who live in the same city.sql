CREATE PROCEDURE [dbo].[Task2_2_5 Get all customers who live in the same city]
AS
	SELECT DISTINCT
		(SELECT custmrs.CompanyName
			FROM Customers AS custmrs
			WHERE custmrs.CustomerID = ordrs.CustomerID) AS Customer,
		ordrs.ShipCity AS 'City'
	FROM Orders AS ordrs
	ORDER BY 'City', Customer
