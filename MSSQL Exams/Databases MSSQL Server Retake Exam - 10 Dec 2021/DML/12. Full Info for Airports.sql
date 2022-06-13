CREATE PROC usp_SearchByAirportName(@airportName VARCHAR(70))
AS
BEGIN
	
	SELECT 
      a.AirportName
	  ,p.FullName
		,CASE
		WHEN fd.TicketPrice <= 400 THEN 'Low'
		WHEN fd.TicketPrice BETWEEN 401 AND 1500 THEN 'Medium'
		WHEN fd.TicketPrice > 1500 THEN 'High'
		END AS LevelOfTickerPrice
	  ,ar.Manufacturer
	  ,ar.Condition
	  ,art.TypeName
  FROM [FlightDestinations] AS fd
  JOIN Airports AS a ON a.Id = fd.AirportId
  JOIN Passengers AS p ON p.Id = fd.PassengerId
  JOIN Aircraft AS ar ON ar.Id = fd.AircraftId
  JOIN AircraftTypes AS art ON art.Id = ar.TypeId
  WHERE a.AirportName = @airportName
  ORDER BY ar.Manufacturer, p.FullName

END