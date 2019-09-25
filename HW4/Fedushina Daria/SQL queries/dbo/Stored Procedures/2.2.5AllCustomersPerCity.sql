CREATE PROCEDURE [dbo].[225AllCustomersPerCity]   @city nvarchar(15)
AS
BEGIN

SELECT [Customers].[ContactName] AS 'AllCustomersPerCity'
FROM [Customers]
WHERE [Customers].[City] =  @city

END


--EXECUTE Northwind.dbo.[225GroupByEmployee] 'México D.F.'