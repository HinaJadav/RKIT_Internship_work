-- VIEW:
-- It is a virtual table based on a SELECT query. It simplifies complex queries.

-- Create a view 'DeveloperDepartment' to get employee details where the role is 'Developer' or 'Senior Developer'.
CREATE VIEW DeveloperDepartment AS
    SELECT 
        ID, Name, DepartmentID, Salary
    FROM
        employee
    WHERE
        Description IN ('Developer', 'Senior Developer');

-- Query the 'DeveloperDepartment' view to retrieve employee details.
SELECT 
    *
FROM
    DeveloperDepartment;

-- Drop the 'DeveloperDepartment' view when no longer needed.
DROP VIEW DeveloperDepartment;
