-- EXISTS Operator
SELECT DepartmentName AS HighestPaidDepartment
FROM departments d
WHERE EXISTS (
        SELECT 1
        FROM employee e
        WHERE e.Description IN ('Developer', 'Senior Developer')
            AND e.DepartmentID = d.DepartmentID
    );
-- ANY and ALL Operators
SELECT DepartmentName as DeveloperDepartment
FROM departments
WHERE DepartmentID = ANY (
        SELECT DepartmentID
        FROM employee
        WHERE Description IN ('developer', 'senior developer', 'HR')
    );
    
SELECT DepartmentName
FROM departments
WHERE DepartmentID = ALL (
        SELECT DepartmentID
        FROM employee
        WHERE Description IN ('developer', 'senior developer', 'HR')
    );