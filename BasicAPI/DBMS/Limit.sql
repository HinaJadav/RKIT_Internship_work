-- LIMIT clause:
-- It is used to restrict the number of rows returned by a query.
-- In this case, the query retrieves the first 2 developers from the employee table.

-- Find the first 2 developers
SELECT 
    Name
FROM
    employee
WHERE
    Description = 'Developer'
LIMIT 2;

-- The OFFSET clause is used in conjunction with LIMIT to skip a specified number of rows.
-- To skip the first 3 developers and then get the next 2 developers:
SELECT 
    Name
FROM
    employee
WHERE
    Description = 'Developer'
LIMIT 2 OFFSET 1;

-- Provide range based limit using (,) which can use to specify a range of rows.
-- To retrieve developers in rows 4 to 6:
SELECT 
    Name
FROM
    employee
WHERE
    Description = 'Developer'
LIMIT 2 , 4;
