CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN
	BEGIN TRANSACTION 
		DECLARE @departmentOfEmp INT = (SELECT [DepartmentId] FROM [Employees] WHERE Id = @EmployeeId)
		DECLARE @departmentOfReport INT = (SELECT c.DepartmentId FROM [Reports] AS r JOIN Categories AS c ON r.CategoryId = c.Id  WHERE r.Id = @ReportId)

		IF(@departmentOfEmp = @departmentOfReport)
		BEGIN
			UPDATE Reports
			SET EmployeeId = @EmployeeId
			WHERE Id = @ReportId
		END
		ELSE
		BEGIN
			ROLLBACK;
			THROW 50001, 'Employee doesn''t belong to the appropriate department!', 1
		END
	COMMIT
END