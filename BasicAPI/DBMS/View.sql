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
    
-- Update View
-- Need to diable to safe update mode because in this mode it try to find one key(primary/ unique) which not yet generated into view table
SET SQL_SAFE_UPDATES = 0; -- Disable safe update mode

UPDATE DeveloperDepartment 
SET 
    Salary = Salary + 1000
WHERE
    ID = 4;

-- Drop the 'DeveloperDepartment' view when no longer needed.
DROP VIEW DeveloperDepartment;


