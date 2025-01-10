SELECT 
    *
FROM
    employee; 
    
-- manually take backup of table

CREATE TABLE employee_copy AS SELECT * FROM employee;

SELECT 
    *
FROM
    employee_copy;
    
drop table employee_copy;

-- command line instruction for backup and restore 

-- Backup

-- Full schema backup 
-- mysqldump -u root -p rkitdatabase > schemaBackup.sql

-- Specific table backup
-- mysqldump -u root -p rkitdatabase employee > tableBackup.sql


-- Restore

-- Restore full schema from backup
-- mysql -u root -p rkitdatabasebackup < schemaBackup.sql

-- Restore specific table from backup
-- mysql -u root -p rkitdatabasebackup < tableBackup.sql

-- explain
EXPLAIN SELECT Name, Age
FROM employee
WHERE Description = 'HR';
-- id: The unique identifier for each step in the execution plan.
-- select_type: Describes the type of query (e.g., SIMPLE, PRIMARY, UNION, etc.).
-- table: The name of the table being accessed in that step.
-- type: The join type used for this operation (e.g., ALL, INDEX, RANGE, etc.).
-- possible_keys: A list of indexes that could be used for the query.
-- key: The index actually used for the query.
-- key_len: The length of the key used (in bytes).
-- ref: Shows which columns or constants are being compared to the key.
-- rows: The estimated number of rows that will be processed for that step.
-- Extra: Additional information about the query execution, such as whether filesort or temporary tables are being used.
