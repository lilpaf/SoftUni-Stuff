SELECT TOP (20) 
	  fd.[Id] AS DestinationId
      ,[Start]
      ,p.FullName
	  ,a.AirportName
      ,[TicketPrice]
  FROM [FlightDestinations] AS fd
  JOIN Airports AS a ON fd.AirportId = a.Id
  JOIN Passengers AS p ON fd.PassengerId = p.Id
  WHERE DAY([Start]) % 2 = 0
  ORDER BY [TicketPrice] DESC, a.AirportName