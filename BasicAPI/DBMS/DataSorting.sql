-- Data sorting using ORDER BY Keyword
-- default order is "ASC"

-- Sort employees in ascending order based on their salaries.
SELECT 
    *
FROM
    employee
ORDER BY Salary;

-- Sort employees in decending order based on their salaries.
SELECT 
    *
FROM
    employee
ORDER BY Salary DESC;

-- Select all employees and sort by Age ascending, then Salary descending
SELECT 
    *
FROM
    employee
ORDER BY Age ASC , Salary DESC;