-- IN Operator for nested query

-- Retrieve the names, descriptions, and salaries of employees who belong to the "Software Development" department.
SELECT 
    Name, Description, Salary
FROM
    employee
WHERE
    DepartmentID IN (SELECT 
            DepartmentID
        FROM
            departments
        WHERE
            DepartmentName = 'Software Development');

