--Найти всех покупателей, которые живут в одном городе.

CREATE PROCEDURE [2.2.5]
AS

SELECT DISTINCT t1.CustomerID, t1.City
FROM Customers t1, Customers t2
WHERE t1.City = t2.City AND t1.CustomerID != t2.CustomerID
ORDER BY t1.City