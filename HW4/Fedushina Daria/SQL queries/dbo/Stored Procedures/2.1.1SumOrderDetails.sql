CREATE PROCEDURE [dbo].[211SumOrderDetails]
AS 
BEGIN

SELECT sum(UnitPrice*Quantity*(1-Discount)) as Total
FROM [dbo].[Order Details]

END