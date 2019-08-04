CREATE PROCEDURE [dbo].[Task2_1_3 Get quantity of different customers with orders using COUNT and witohut WHERE and GROUP]
AS
	SELECT COUNT(DISTINCT CustomerID) FROM Orders
