CREATE PROC usp_SearchForFiles(@fileExtension VARCHAR(50))
AS
BEGIN
	SELECT
	   [Id]
      ,[Name]
      ,CONCAT([Size], 'KB') AS Size  
		FROM [Files]
		WHERE [Name] LIKE CONCAT('%', @fileExtension)
		ORDER BY Id, [Name], Size DESC
END