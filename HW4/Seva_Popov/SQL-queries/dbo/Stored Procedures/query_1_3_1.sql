﻿CREATE PROCEDURE [query_1_3_1]
AS
	SELECT DISTINCT OrderID
    	FROM [Order Details]
    	WHERE Quantity BETWEEN 3 AND 10
