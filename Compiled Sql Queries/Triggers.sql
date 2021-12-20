CREATE OR ALTER TRIGGER Supervisor_delete_trigger ON Staff
INSTEAD OF DELETE
AS
DECLARE
	@deletedStaffID INT;
BEGIN
	UPDATE Staff 
	Set Supervisor_ID = NULL 
	WHERE Supervisor_ID IN (SELECT StaffID FROM Deleted);
	PRINT('Referenced SupervisorIDs set NULL');

	DELETE FROM Staff
	WHERE StaffID IN (SELECT StaffID FROM Deleted);
	PRINT('Deleted the row');
END;

CREATE OR ALTER TRIGGER Account_delete_cascade_trigger ON Accounts
INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Customers
	WHERE AccountNo IN (SELECT AccountNo FROM Deleted);
	PRINT('Customer with accountNo deleted');

	DELETE FROM Staff
	WHERE AccountNo IN (SELECT AccountNo FROM Deleted);
	PRINT('Staff with accountNo deleted');

	DELETE FROM Accounts
	WHERE AccountNo IN (SELECT AccountNo FROM Deleted);
	PRINT('Deleted the row');
END;