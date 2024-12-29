-- TCL (Transaction Control Language):
-- TCL commands manage transactions in a database. Common TCL commands are COMMIT, ROLLBACK, and SAVEPOINT.

-- Begin a transaction
START TRANSACTION;

-- Insert data into 'employee' table
INSERT INTO `rkitdatabase`.`employee` (Name, Description, Salary, Email, Age, DepartmentID)
VALUES ('Hema Maheta', 'Developer', 50000, 'hema@gmail.com', 23, 1);

-- Commit the transaction (save the changes to the database)
COMMIT;

-- Rollback the transaction (undo changes made before COMMIT)
-- ROLLBACK;

-- Save a point in the transaction (can roll back to this point)
SAVEPOINT savepoint1;

-- Rollback to a specific savepoint
-- ROLLBACK TO SAVEPOINT savepoint1;
