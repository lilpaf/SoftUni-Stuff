SELECT 
		CONCAT(e.FirstName, ' ', e.LastName) AS FullName
		,COUNT(DISTINCT r.UserId) AS UsersCount
  FROM [Reports] AS r
  JOIN Users AS u ON r.UserId = u.Id
  RIGHT JOIN Employees AS e ON e.Id = r.EmployeeId
  GROUP BY r.EmployeeId, e.FirstName, e.LastName
  ORDER BY UsersCount DESC , FullName