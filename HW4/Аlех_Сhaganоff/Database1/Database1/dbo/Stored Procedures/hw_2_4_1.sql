CREATE PROCEDURE dbo.hw_2_4_1
      
AS   


select CompanyName
from Suppliers
where supplierID in
(select SupplierID
from Products
where UnitsInStock = 0)

