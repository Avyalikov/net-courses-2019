CREATE PROCEDURE [dbo].[2_4_3_ZeroOrdersExists]
AS
	SELECT cust.ContactName
    FROM Customers as cust
    WHERE NOT EXISTS 
      (SELECT cust.ContactName
      FROM Orders as ord
      WHERE cust.CustomerID = ord.CustomerID)
