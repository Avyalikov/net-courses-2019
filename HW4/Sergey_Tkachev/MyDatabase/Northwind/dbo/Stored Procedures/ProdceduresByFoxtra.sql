CREATE PROCEDURE [dbo].[ProdceduresByFoxtra]
AS
	--1.1.1
	Select [OrderID]
		  ,[ShippedDate]
		  ,[ShipVia]
	  from [dbo].[Orders]
	  where [ShippedDate] > '1998-05-06'
	  and [ShipVia] >= 2;

  --1.1.2
  Select [OrderID], 
  case 
	  when [ShippedDate] IS NULL 
	  then N'Not Shipped' 
  end as ShippedDate
  from [dbo].[Orders]
  where [ShippedDate] is null;

  --1.1.3
  Select [OrderID] as N'Order Number',
  case 
	  when [ShippedDate] IS NULL 
	  then N'Not Shipped'
	  else cast ([ShippedDate] AS VARCHAR(50) ) 
  end as N'Shipped Date'
  from [dbo].[Orders]
  where [ShippedDate] > '1998-05-06'
  or [ShippedDate] is null;
  
  --1.2.1
  SELECT [ContactName]
      ,[Country]
  FROM [dbo].[Customers]
  where [Country] in ('USA', 'Canada')
  order by [ContactName], [Country]

  --1.2.2
  SELECT [ContactName]
      ,[Country]
  FROM [dbo].[Customers]
  where [Country] not in ('USA', 'Canada')
  order by [ContactName]

  --1.2.3
  SELECT distinct [Country]
  FROM [dbo].[Customers]
  order by [Country] desc;

  --1.3.1
  SELECT distinct [OrderID]
  FROM [dbo].[Order Details]
  where [Quantity] between 3 and 10;

  --1.3.2
  SELECT [CustomerID]
      ,[Country]
  FROM [dbo].[Customers]
  where lower ( substring ( [Country], 1, 1 ) ) between 'b' and 'g'
  order by [Country];

  --1.3.3
  SELECT [CustomerID]
      ,[Country]
  FROM [dbo].[Customers]
  where lower ( substring ( [Country], 1, 1 ) ) like '[b-g]'
  order by [Country];

  --1.4
  SELECT [ProductName]
  FROM [dbo].[Products]
  where  lower ( [ProductName] ) like '%cho[a-z0-9]olade%';

  --2.1.1
    SELECT Sum( [UnitPrice] * (1 - [Discount]) * [Quantity]  ) as 'Totals'
    FROM [dbo].[Order Details];

--2.1.2 
  Select (Count(OrderID) - Count([ShippedDate])) as N'Amount of undelevered orders'
  from [dbo].[Orders];

--2.1.3
  Select (Count(distinct CustomerID) ) as N'Amount of customers'
  from [dbo].[Orders];	

--2.2.1
  Select year([OrderDate]) as N'Year', (Count(OrderID) ) as N'Total'
  from [dbo].[Orders]
  group by year([OrderDate]);	

  Select Count(OrderID)
  from [dbo].[Orders];

--2.2.2
  Select 
	  (SELECT [LastName] + ' ' + [FirstName]
	  FROM [dbo].[Employees]
	  where  [Employees].[EmployeeID] = [Orders].[EmployeeID]) as Seller,
	  Count(OrderID) as Amount
  from [dbo].[Orders]
  group by  [Orders].[EmployeeID]
  order by Amount desc;
  
--2.2.3
  Select 
	  (SELECT [LastName] + ' ' + [FirstName]
	  FROM [dbo].[Employees]
	  where  [Employees].[EmployeeID] = [Orders].[EmployeeID]) as Seller,
	  [Orders].[CustomerID],
	  Count(OrderID) as Amount
  from [dbo].[Orders]
  where year([OrderDate]) = 1998
  group by  [Orders].[EmployeeID], [Orders].[CustomerID]
  order by Amount desc;

--2.2.4
	Select  
		[Employees].[LastName] + ' ' + [Employees].[FirstName] as Seller
		,[Customers].[ContactName]
		,[Employees].[City]
	FROM [dbo].[Employees], [dbo].[Customers]
	where  [Customers].[City] = [Employees].[City];
	
--2.2.5
	select 
	[ContactName],
	[City]
	from [dbo].[Customers] 
	where [City] in (
			select [City]
			from [dbo].[Customers]
			group by [City]
			having count([City]) > 1)
	group by [City], [ContactName]

--2.2.6
	Select  
		e1.[LastName] + ' ' + e1.[FirstName] as Employee
		,(SELECT [LastName] + ' ' + [FirstName]
		  FROM [dbo].[Employees] as e2
		  where  e1.[ReportsTo] = e2.[EmployeeID]) as Manager
	FROM [dbo].[Employees] as e1;
	
--2.3.1
	Select distinct
		e.[LastName] + ' ' + e.[FirstName] as Employee
		,r.[RegionDescription]
	FROM [dbo].[Employees] as e
	inner join [EmployeeTerritories] as et
			on (e.[EmployeeID] = et.[EmployeeID])
	inner join [Territories] as t
			on et.[TerritoryID] = t.[TerritoryID]
	inner join [Region] as r
		on t.[RegionID] = r.[RegionID]
	where r.[RegionDescription] = 'Western';

--2.3.2
	Select c.[ContactName], Count(o.OrderID) as 'Amount of orders'
	FROM [dbo].[Customers] c
	left join [dbo].[Orders] o
	on c.[CustomerID] = o.[CustomerID]
	group by c.[ContactName]
	order by 'Amount of orders';

--2.4.1
  SELECT s.[CompanyName]
  FROM [dbo].[Suppliers] s
  where s.[SupplierID] 
  in(SELECT p.[SupplierID]
	FROM [dbo].[Products] p
	where  p.[UnitsInStock] = 0); 

--2.4.2
	Select  
	e.[LastName] + ' ' + e.[FirstName] as Employee
	FROM [dbo].[Employees] as e
	where e.[EmployeeID] 
	in(SELECT o.[EmployeeID]
	FROM [dbo].[Orders] o
	group by o.[EmployeeID]
	having count(o.OrderID) > 150); 

--2.4.3
	Select c.[ContactName]
	FROM [dbo].[Customers] c
	where not exists
	(SELECT o.[CustomerID]
	FROM [dbo].[Orders] o
	where c.[CustomerID] = o.[CustomerID]);


