SELECT 
      p.FullName
	  ,COUNT(fd.AircraftId) AS CountOfAircraft
      ,SUM([TicketPrice]) AS TotalPayed
  FROM [FlightDestinations] AS fd
  JOIN Passengers AS p ON fd.PassengerId = p.Id
  WHERE SUBSTRING(p.FullName,2,1) = 'a'
  GROUP BY fd.PassengerId, p.FullName
  HAVING COUNT(fd.AircraftId) > 1
  ORDER BY p.FullName