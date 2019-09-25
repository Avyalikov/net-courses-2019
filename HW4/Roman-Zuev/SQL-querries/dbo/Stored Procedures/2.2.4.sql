--Найти покупателей и продавцов, которые живут в одном городе.
--Если в городе живут только один или несколько продавцов, 
--или только один или несколько покупателей, то информация о таких 
--покупателя и продавцах не должна попадать в результирующий набор. 
--Не использовать конструкцию JOIN.

CREATE PROCEDURE [2.2.4]
AS

SELECT Employees.City, EmployeeID, CustomerID
FROM Employees, Customers
WHERE Employees.City = Customers.City
