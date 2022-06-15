CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(30))
RETURNS INT
AS
BEGIN
		RETURN
		(SELECT 
	     COUNT(*)
         FROM [Commits]
	     WHERE [ContributorId] = (SELECT [Id] FROM [Users] WHERE Username = @username)
		)
END