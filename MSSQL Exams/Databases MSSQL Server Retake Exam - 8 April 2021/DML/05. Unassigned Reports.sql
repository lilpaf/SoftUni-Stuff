SELECT 
      [Description]
      ,FORMAT([OpenDate], 'dd-MM-yyyy') AS OpenDate
  FROM [Reports] AS r
  WHERE [EmployeeId] IS NULL
  ORDER BY r.OpenDate, [Description]