SELECT 
      [Username]
      ,AVG(f.Size) AS Size
  FROM [Users] AS u
  JOIN [Commits] AS c ON c.ContributorId = u.Id
  JOIN [RepositoriesContributors] AS cr ON cr.ContributorId = u.Id
  JOIN [Files] AS f ON f.CommitId = c.Id
  GROUP BY [Username]
  HAVING COUNT(c.Id) > 0
  ORDER BY Size DESC, [Username]