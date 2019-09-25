CREATE PROCEDURE [dbo].[Task2_3_2 Get names of customers and quantity of them orders]
AS
	SELECT 
		custmrs.CompanyName,
		(SELECT COUNT(ordrs.OrderID) 
			FROM Orders AS ordrs 
			WHERE ordrs.CustomerID = custmrs.CustomerID) AS Amount
	FROM Customers AS custmrs
	ORDER BY Amount
