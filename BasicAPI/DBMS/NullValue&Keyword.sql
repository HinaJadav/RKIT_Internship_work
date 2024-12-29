-- NULL Values: NULL represents a missing or undefined value.
-- Queries can check for NULL values using 'IS NULL' or 'IS NOT NULL' conditions.

-- Find employees where the DepartmentID is NULL (i.e., employees without a department assigned)
SELECT 
    *
FROM
    employee
WHERE
    DepartmentID IS NULL;

-- Find employees where the DepartmentID is NOT NULL (i.e., employees assigned to a department)
SELECT 
    *
FROM
    employee
WHERE
    DepartmentID IS NOT NULL;
