USE Northwind;
SELECT
   cstmrs.CompanyName
FROM Customers AS cstmrs
WHERE NOT EXISTS (SELECT ordrs.OrderID
                  FROM Orders AS ordrs
                  WHERE ordrs.CustomerID = cstmrs.CustomerID);
