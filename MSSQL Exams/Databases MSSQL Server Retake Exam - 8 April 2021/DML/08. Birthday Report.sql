SELECT 
	u.Username
	,c.[Name] AS CategoryName 
  FROM [Reports] AS r
  JOIN Categories AS c ON r.CategoryId = c.Id
  JOIN Users AS u ON u.Id = r.UserId
  WHERE (DAY(r.OpenDate) = DAY(u.Birthdate) AND MONTH(r.OpenDate) = MONTH(u.Birthdate)) OR (DAY(r.CloseDate) = DAY(u.Birthdate) AND MONTH(r.CloseDate) = MONTH(u.Birthdate))
  ORDER BY Username, CategoryName