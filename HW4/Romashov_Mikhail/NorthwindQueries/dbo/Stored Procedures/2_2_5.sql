--Найти всех покупателей, которые живут в одном городе.

CREATE PROCEDURE [query_2_2_5]
AS

SELECT DISTINCT x.ContactName as 'Name', x.City
	FROM Customers as x, Customers as y
	WHERE x.City = y.City AND x.ContactName != y.ContactName
	ORDER BY x.City