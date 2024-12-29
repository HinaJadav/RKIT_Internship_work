-- DQL: Deals with querying data.
-- Command: SELECT.

-- Select all records from the 'employee' table
SELECT 
    *
FROM
    `rkitdatabase`.`employee`;

-- Select specific columns (Name, Salary) from 'employee' table
SELECT 
    Name, Salary
FROM
    `rkitdatabase`.`employee`;

-- Select employees with a specific description (e.g., 'Developer')
SELECT 
    *
FROM
    `rkitdatabase`.`employee`
WHERE
    Decription = 'Developer';

-- Select all records from 'department' table
SELECT 
    *
FROM
    department;

-- Select departments with more than 5 employees
SELECT 
    *
FROM
    department
WHERE
    NoOfEmployees > 5;
