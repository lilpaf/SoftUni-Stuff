SELECT 
	CASE 
	WHEN COALESCE(e.FirstName,e.LastName) IS NOT NULL
	THEN CONCAT(e.FirstName,' ',e.LastName)
	ELSE
	'None'
	END AS Employee,
	ISNULL(d.Name,'None') AS Department,
	ISNULL(c.Name,'None') AS Category,
	ISNULL(r.Description,'None') AS [Description],
	ISNULL(FORMAT(r.OpenDate, 'dd.MM.yyyy'),'None') AS [OpenDate],
	ISNULL(s.Label,'None') AS [Status],
	ISNULL(u.Name, 'None') AS [User]
FROM Reports r 
LEFT JOIN Employees e ON e.Id=r.EmployeeId
LEFT JOIN Categories c ON c.Id=r.CategoryId
LEFT JOIN Departments d ON d.Id=e.DepartmentId
LEFT JOIN STATUS s ON s.Id=r.StatusId
LEFT JOIN Users u ON u.Id=r.UserId
ORDER BY e.FirstName DESC,e.LastName DESC,Department, Category, [Description], r.OpenDate,
[Status],[User] 