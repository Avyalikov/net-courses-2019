CREATE PROCEDURE [dbo].[Task2_4_3 Get customers without orders using EXISTS]
AS
	SELECT cstmrs.CompanyName
	FROM Customers AS cstmrs
	WHERE NOT EXISTS (SELECT ordrs.OrderID
						FROM Orders AS ordrs
						WHERE ordrs.CustomerID = cstmrs.CustomerID)
