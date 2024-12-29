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