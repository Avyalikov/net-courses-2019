CREATE PROCEDURE [dbo].[GetOrders](
	@StartShippingDate DATETIME,
	@ShipViaMinimal INT
)
AS 
SELECT 
	  [OrderID]
      ,[CustomerID]
      ,[EmployeeID]
      ,[OrderDate]
      ,[RequiredDate]
      ,[ShippedDate]
      ,[ShipVia]
      ,[Freight]
      ,[ShipName]
      ,[ShipAddress]
      ,[ShipCity]
      ,[ShipRegion]
      ,[ShipPostalCode]
      ,[ShipCountry]
FROM [dbo].[Orders]
WHERE [ShippedDate] > @StartShippingDate
AND [ShipVia] >= @ShipViaMinimal


