--Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.

CREATE PROCEDURE [2.4.2]
AS

SELECT CONCAT (LastName, ' ', FirstName) AS 'Seller'
FROM Employees
WHERE EmployeeID IN
(SELECT EmployeeID FROM Orders
GROUP BY EmployeeID
HAVING COUNT(OrderID) > 150)