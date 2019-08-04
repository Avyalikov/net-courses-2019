CREATE PROCEDURE [dbo].[1_4_1_ProductsLike]
AS
	SELECT ProductName
    FROM Products
    WHERE ProductName LIKE '%cho_olade%'
