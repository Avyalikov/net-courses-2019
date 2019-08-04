CREATE PROCEDURE [dbo].[Task2_2_3 Get quantity orders of every seller for every customer in 1998]
AS
	SELECT
		(SELECT CONCAT( employs.LastName, ' ', employs.FirstName)
			FROM Employees AS employs
			WHERE employs.EmployeeID = ordrs.EmployeeID) AS Seller,
		(SELECT custmrs.CompanyName
			FROM Customers AS custmrs
			WHERE custmrs.CustomerID = ordrs.CustomerID) AS Customer,
		COUNT(ordrs.OrderID) AS Ammount
	FROM Orders AS ordrs
	WHERE YEAR(ordrs.OrderDate)=1998
	GROUP BY ordrs.EmployeeID, ordrs.CustomerID
	--ORDER BY Seller, Customer
