SELECT 
	   i.[Id]
      ,CONCAT(u.[Username], ' : ', Title) AS IssueAssignee
  FROM [Issues] AS i
  JOIN Users AS u ON i.AssigneeId = u.Id
  ORDER BY i.[Id] DESC, IssueAssignee