SELECT 
      [FirstName]
      ,[LastName]
      ,a.Manufacturer
	  ,a.Model
	  ,a.FlightHours
  FROM [Pilots] AS p
  JOIN PilotsAircraft AS pa ON pa.PilotId = p.Id
  JOIN Aircraft AS a ON pa.AircraftId = a.Id
  WHERE a.FlightHours IS NOT NULL AND a.FlightHours < 304
  ORDER BY a.FlightHours DESC, [FirstName]