-- Aliases: Assign temporary names to tables or columns for easier reference.
-- Example: Assigning an alias to a column
SELECT 
    Name AS DevelopersName 
FROM
    employee
WHERE
    Description = 'Developer';

-- Assigning aliases to tables and columns in a join
SELECT 
    Name AS AllEmployees
FROM
    employee AS e, departments AS d
WHERE
    e.DepartmentID = d.DepartmentID;

-- Joins: Combine rows from two or more tables based on related columns.
-- Inner Join: Select employees with their department details
SELECT 
    e.Name, e.Description, e.Email, d.DepartmentID, d.DepartmentName
FROM
    employee e
INNER JOIN 
    departments d
ON 
    e.DepartmentID = d.DepartmentID;

-- Left Join: Select all employees and their department details (if any)
SELECT 
    e.Name, e.Description, e.Email, d.DepartmentID, d.DepartmentName
FROM
    employee e
LEFT JOIN 
    departments d
ON 
    e.DepartmentID = d.DepartmentID;

-- Right Join: Select all departments and their employees (if any)
SELECT 
    e.Name, e.Description, e.Email, d.DepartmentID, d.DepartmentName
FROM
    employee e
RIGHT JOIN 
    departments d
ON 
    e.DepartmentID = d.DepartmentID;

-- Outer Join: Cartesian product of employees and departments
SELECT 
    e.Name, e.Description, e.Email, d.DepartmentID, d.DepartmentName
FROM
    employee e
CROSS JOIN 
    departments d
ON 
    e.DepartmentID = d.DepartmentID;

-- Self Join: Select employees earning the same salary as others
SELECT 
    e1.Name, e1.Salary AS SameSalaryEmployeeName
FROM
    employee AS e1, employee AS e2
WHERE
    e1.Salary = e2.Salary AND e1.ID != e2.ID;

-- Union: Combine the result sets of two or more SELECT statements.
-- Duplicate Removal
-- Example: Combine job descriptions from employees and department names
SELECT 
    Description
FROM
    employee
UNION
SELECT 
    DepartmentName
FROM
    departments;
    
-- Union all: 
-- Not remove duplicate show all result 
SELECT 
    Description
FROM
    employee
UNION ALL
SELECT 
    DepartmentName
FROM
    departments;

