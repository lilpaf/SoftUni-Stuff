SELECT TOP (5) 
      [Name] AS CategoryName
      ,COUNT(r.Id) AS ReportsNumber
  FROM [Categories] AS c
  JOIN Reports AS r ON r.CategoryId = c.Id
  GROUP BY c.Id, [Name]
  ORDER BY ReportsNumber DESC, CategoryName