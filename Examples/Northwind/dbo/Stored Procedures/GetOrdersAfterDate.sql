CREATE PROCEDURE [dbo].[GetOrdersAfterDate] (
	@StartDate DateTime,
	@ShipViaMin INT
)
AS
SELECT [OrderID]
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
  WHERE [ShippedDate] > @StartDate
  AND [ShipVia] > @ShipViaMin
