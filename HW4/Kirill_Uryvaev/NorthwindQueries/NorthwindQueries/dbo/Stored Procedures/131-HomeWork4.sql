CREATE PROCEDURE [dbo].[HomeWork4]
AS
/*1.1.1*/
	SELECT OrderID, ShippedDate, ShipVia FROM Orders WHERE ShipVia >= 2 AND ShippedDate>='5/6/1998';
/*1.1.2*/
	SELECT OrderID, CASE WHEN ShippedDate IS NULL THEN 'Not shipped' END AS ShippedDate FROM Orders WHERE ShippedDate IS NULL;
/*1.1.3*/
	SELECT OrderID AS 'Order Number', 
	CASE WHEN ShippedDate IS NULL 
	THEN 'Not shipped' 
	ELSE CONVERT(varchar,ShippedDate,101) 
	END AS 'Shipped Date' 
	FROM Orders 
	WHERE ShippedDate IS NULL OR ShippedDate>'5/6/1998';
/*1.2.1*/
	SELECT ContactName, Country
	FROM Customers 
	WHERE Country IN ('USA', 'Canada')
	ORDER BY ContactName, Country;
/*1.2.2*/
	SELECT ContactName, Country
	FROM Customers 
	WHERE Country NOT IN ('USA', 'Canada')
	ORDER BY ContactName;
/*1.2.3*/
	SELECT DISTINCT Country
	FROM Customers 
	WHERE CustomerID IS NOT NULL
	ORDER BY Country DESC;
/*1.3.1*/
	SELECT DISTINCT OrderID 
	FROM [Order Details] 
	WHERE Quantity  BETWEEN 3 AND 10;
/*1.3.2*/
	SELECT DISTINCT CustomerID, Country 
	FROM Customers 
	WHERE SUBSTRING(Country, 1, 1)  BETWEEN 'b' AND 'g'
	ORDER BY Country;
/*1.3.3*/
	SELECT DISTINCT CustomerID, Country 
	FROM Customers 
	WHERE SUBSTRING(Country, 1, 1)  >= 'b' AND SUBSTRING(Country, 1, 1)  <= 'g'
	ORDER BY Country;
/*1.4*/
	SELECT DISTINCT ProductName 
	FROM Products  
	WHERE ProductName LIKE '%cho_olade%';
/*2.1.1*/
	SELECT SUM(UnitPrice*Quantity*(1-Discount)) as 'Totals' 
	FROM [Order Details]  
/*2.1.2*/
	SELECT COUNT(OrderDate) - COUNT(ShippedDate) as 'Not shipped'
	FROM Orders;
/*2.1.3*/
	SELECT COUNT(DISTINCT CustomerID) as 'Amount of customers'
	FROM Orders;
/*2.2.1*/
	SELECT YEAR(OrderDate) as 'Year', COUNT(YEAR(OrderDate)) as 'Total'
	FROM Orders
	GROUP BY YEAR(OrderDate);
/*Checking*/
	SELECT COUNT(OrderDate)
	FROM Orders;
/*2.2.2*/
	SELECT (SELECT CONCAT(LastName,' ', FirstName) FROM Employees WHERE EmployeeID = Orders.EmployeeID) as 'Seller', COUNT(EmployeeID) as 'Amount'
	FROM Orders
	GROUP BY EmployeeID
	ORDER BY COUNT(EmployeeID) DESC
/*2.2.3*/
	SELECT EmployeeID, CustomerID, COUNT(OrderDate) as 'Orders'
	FROM Orders
	WHERE YEAR(OrderDate) = '1998'
	GROUP BY EmployeeID, CustomerID
/*2.2.4*/
	SELECT Employees.City, CONCAT(LastName,' ', FirstName) as 'Employee', ContactName as 'Customer'
	FROM Customers, Employees
	WHERE Employees.City = Customers.City
	ORDER BY Employees.City
/*2.2.5*/
	SELECT City, ContactName as 'Customer'
	FROM Customers c
	WHERE (SELECT COUNT(CustomerID)
	FROM Customers
	WHERE City = c.City
	GROUP BY Customers.City) > 1
	ORDER BY City
/*2.2.6*/
	SELECT CONCAT(e.LastName,' ',e.FirstName) as 'Employee', (SELECT CONCAT(LastName,' ', FirstName) FROM Employees WHERE EmployeeID = e.ReportsTo) as 'Supervisor'
	FROM Employees e
/*2.3.1*/
	SELECT DISTINCT CONCAT(FirstName, ' ', LastName) as 'Employee'
	FROM Employees
	INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
	INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
	INNER JOIN Region ON Region.RegionID = Territories.RegionID
	WHERE Region.RegionDescription = 'Western'
/*2.3.2*/
	SELECT Customers.ContactName, COUNT(OrderID) as 'Orders'
	FROM Customers
	LEFT JOIN Orders ON Orders.CustomerID = Customers.CustomerID
	GROUP BY Customers.CustomerID, Customers.ContactName
	ORDER BY COUNT(OrderID)
/*2.4.1*/
	SELECT CompanyName
	FROM Suppliers
	WHERE 0 IN (SELECT UnitsInStock  FROM Products WHERE Suppliers.SupplierID = Products.SupplierID)
/*2.4.2*/
	SELECT CONCAT(FirstName, ' ', LastName) as 'Employee'
	FROM Employees
	WHERE (SELECT COUNT(ShippedDate)  FROM Orders WHERE Orders.EmployeeID = Employees.EmployeeID GROUP BY Orders.EmployeeID)>150
/*2.4.3*/
	SELECT ContactName
	FROM Customers
	WHERE NOT EXISTS (SELECT ShippedDate FROM Orders WHERE Orders.CustomerID = Customers.CustomerID ) 
RETURN 0
