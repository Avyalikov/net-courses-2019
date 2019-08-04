CREATE PROCEDURE [dbo].[Task2_2_6 Get boss for all employees]
/*
'Sales Representative' < 'Sales Manager' < 'Inside Sales Coordinator' < 'Vice President, Sales'
                  'Sales Representative' < 'Inside Sales Coordinator' < 'Vice President, Sales'
*/
AS
	SELECT
		CONCAT( employs.LastName, ' ', employs.FirstName) AS Seller,   
		employs.Title, 
		employs.City,
		'Boss' = 		
			CASE
				WHEN employs.Title = 'Sales Representative' AND 
					(SELECT Title
						FROM Employees
						WHERE City = employs.City AND Title = 'Sales Manager') IS NOT NULL /* If it have Sales Manager in city then Sales Manager is boss*/
					THEN (SELECT CONCAT( bosses.LastName, ' ', bosses.FirstName, ', ', bosses.Title, ', ', bosses.City)
							FROM Employees AS bosses
							WHERE bosses.Title = 'Sales Manager')
				WHEN employs.Title = 'Sales Representative'
					THEN (SELECT CONCAT( bosses.LastName, ' ', bosses.FirstName, ', ', bosses.Title, ', ', bosses.City)
							FROM Employees AS bosses
							WHERE bosses.Title = 'Inside Sales Coordinator')
				WHEN employs.Title = 'Sales Manager'
					THEN (SELECT CONCAT( bosses.LastName, ' ', bosses.FirstName, ', ', bosses.Title, ', ', bosses.City)
						FROM Employees AS bosses
						WHERE bosses.Title = 'Inside Sales Coordinator')
				WHEN employs.Title = 'Inside Sales Coordinator'
					THEN (SELECT CONCAT( bosses.LastName, ' ', bosses.FirstName, ', ', bosses.Title, ', ', bosses.City)
						FROM Employees AS bosses
						WHERE bosses.Title = 'Vice President, Sales')
				ELSE 'President'
			END
	FROM Employees AS employs
	ORDER BY employs.City, Seller
