CREATE PROCEDURE [dbo].[Procedure_2.4.3]
AS
	SELECT ContactName 
	FROM Customers Cst
	WHERE NOT EXISTS
		(SELECT ContactName
		FROM Orders Ord
		WHERE Cst.CustomerID = Ord.CustomerID)
