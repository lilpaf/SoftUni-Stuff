SELECT 
	   a.Id AS AircraftId
      ,[Manufacturer]
      ,[FlightHours]
	  ,COUNT(fd.PassengerId) AS FlightDestinationsCount
	  ,ROUND(AVG(fd.TicketPrice), 2)
  FROM [Aircraft] AS a
  JOIN [FlightDestinations] AS fd ON fd.AircraftId = a.Id
  GROUP BY a.[Id], [Manufacturer] ,[FlightHours]
  HAVING COUNT(fd.PassengerId) >= 2
  ORDER BY FlightDestinationsCount DESC, a.Id