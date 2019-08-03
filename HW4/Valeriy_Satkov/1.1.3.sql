USE Northwind;
--DECLARE @date datetime = '1996-07-16'; -- TEST DATE
DECLARE @date datetime = '1998-05-06';
SELECT
   ordrs.OrderID AS 'Order Number',   
   ordrs.OrderDate AS 'Order Date',
   ordrs.ShippedDate AS 'Shipped Date'
FROM Orders AS ordrs
WHERE ordrs.ShippedDate > @date
/*ORDER BY 'Order Date'*/;
