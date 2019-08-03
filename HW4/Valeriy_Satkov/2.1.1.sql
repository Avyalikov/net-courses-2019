USE Northwind;
SELECT SUM(UnitPrice * Quantity * (1 - Discount)) AS Totals FROM [Order Details];
