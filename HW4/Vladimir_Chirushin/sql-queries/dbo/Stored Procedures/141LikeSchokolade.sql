-- Task 1.4 #1
CREATE PROCEDURE LikeSchokolade
AS
SELECT 
	ProductName
FROM 
	dbo.Products
WHERE 
	ProductName LIKE '%cho_olade%'