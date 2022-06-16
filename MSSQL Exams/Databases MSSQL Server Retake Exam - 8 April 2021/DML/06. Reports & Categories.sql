SELECT 
      [Description]
      ,c.[Name]
  FROM [Reports] AS r
  JOIN Categories AS c ON c.Id = r.CategoryId
  WHERE r.CategoryId IS NOT NULL
  ORDER BY [Description] ,c.[Name]