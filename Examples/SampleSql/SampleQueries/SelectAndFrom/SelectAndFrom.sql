-- From samples
select * 
from Categories

select cat.*
from Categories cat

select Categories.*
from Categories 


-- Select samples
select CategoryID, CategoryName
from Northwind.Categories


select cat.CategoryID, cat.CategoryName
from Northwind.Categories as cat 


-- Select expressions
select 
	p.ProductName, 
	p.UnitPrice * (p.UnitsInStock + p.UnitsOnOrder) as SummaryCosts
from Northwind.Products p


select GETDATE() as Now, 2 + 5 * 6 as Expr
