--Найти покупателей и продавцов, которые живут в одном городе. 
--Если в городе живут только один или несколько продавцов, или только один или несколько 
--покупателей, то информация о таких покупателя и продавцах не должна попадать в 
--результирующий набор. Не использовать конструкцию JOIN.

CREATE PROCEDURE [query_2_2_4]
AS

SELECT CompanyName as 'Company', FirstName as 'Name'
	FROM Customers, Employees
	WHERE Customers.City = Employees.City