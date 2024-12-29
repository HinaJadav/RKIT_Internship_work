-- Aggregation functions:
-- Aggregation functions in databases are used to perform calculations on sets of data, returning a single value as output. 
-- These functions are essential for summarizing and analyzing large datasets, helping to derive insights and make informed decisions.

-- Types of Aggregation Functions

-- COUNT: Returns the number of rows that match a specified condition.
-- Find total number of employees
SELECT 
    COUNT(ID)
FROM
    employee
WHERE
    Description = 'Developer';
    
-- AVG: Computes the average value of a column or expression.
-- Find average salary of employees
SELECT 
    AVG(Salary)
FROM
    employee
WHERE
    Description = 'Developer';

-- SUM: Calculates the total value of a column or expression.
-- Find total salary amount company needs to pay to all employees
SELECT 
    SUM(Salary)
FROM
    employee;

-- MIN: Returns the minimum value in a column or expression.
-- Find developer's minimum salary
SELECT 
    MIN(Salary)
FROM
    employee
WHERE
    Description = 'Developer';
    
-- MAX: Returns the maximum value in a column or expression.
-- Find developer's maximum salary
SELECT 
    MAX(Salary)
FROM
    employee
WHERE
    Description = 'Developer';
    
-- GROUP BY: Groups rows based on one or more columns and applies aggregation functions to each group.
-- Count employees grouped by their job descriptions
SELECT 
    COUNT(ID), Description
FROM
    employee
GROUP BY Description;

-- HAVING: Filters groups based on conditions applied to aggregated values.
-- Find job descriptions with more than two employees
SELECT 
    COUNT(ID), Description
FROM
    employee
GROUP BY Description
HAVING COUNT(ID) > 2;
